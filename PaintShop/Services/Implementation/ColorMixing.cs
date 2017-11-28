using PaintShop.Model;
using PaintShop.Services.Repository;
using PaintShop.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PaintShop.StaticData.ErrorEnum;

namespace PaintShop.Services.Implementation
{
    public class ColorMixing : IColorMixing
    {
        #region Private variables
        private IEnumerable<string> possibleCombinationOfColors;
        private IList<OutPutModel> listofPossibleOutPuts;
        private IList<string> customerString;
        private InputModel inputModel;
        private readonly IReadFile readFile;

        #endregion

        #region Constructor
        public ColorMixing(IReadFile ReadFile)
        {
            readFile = ReadFile;
        }
        #endregion

        #region Public Method
        public string GetCustomerColors(string filePath)
        {
            try
            {                
                IEnumerable<string> inputLines = readFile.ReadLine(filePath);
                readCustomerandColor(inputLines);
                generateOutPut();
                return takeLessExpensiveOutPut();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Convert the file text line into the inputModel
        /// </summary>
        /// <param name="inputLines"></param>
        /// <returns></returns>
        private InputModel readCustomerandColor(IEnumerable<string> inputLines)
        {
            if (inputLines == null)
                throw new Exception(Errors.NoCustomerColorPresent.GetDescription());


            inputModel = new InputModel();
            foreach (string line in inputLines)
            {
                if (!inputModel.IsFirstInputLineParse)
                {
                    inputModel.OutPutColorCount = int.Parse(line.Trim());
                }
                else
                {
                    inputModel.CustomerInputLines.Add(parseCustomer(line));
                }
            }
            return inputModel;
        }

        private CustomerInputLine parseCustomer(string line)
        {
            if (((line == null)
                        || line.Equals("")))
            {
                throw new Exception(Errors.NoCustomerColorPresent.GetDescription());
            }

            try
            {
                CustomerInputLine customers = new CustomerInputLine();
                CustomerModel customer;
                //  'colors' will be similar to: [1,M,2,G,5,M]
                String[] colors = line.Split(' ');
                for (int i = 0; (i < colors.Length); i += 2)
                {
                    customer = new CustomerModel();
                    int color = int.Parse(colors[i]);
                    string finishStr = colors[(i + 1)];
                    customer.Paint = color;
                    customer.ColorFinish = ColorEnums.parseColorFinish(finishStr);
                    customers.CustomerColorLine.Add(customer);
                }
                return customers;
            }
            catch
            {
                throw new Exception(Errors.InputCustomerColorNotExpectedFormat.GetDescription());
            }


        }

        /// <summary>
        /// formate and generate the out put
        /// </summary>
        /// <returns></returns>
        private string generateOutPut()
        {
            StringBuilder sb = new StringBuilder();
            generatePossibleCombination(inputModel.OutPutColorCount);
            for (int i = 1; i <= inputModel.OutPutColorCount; i++)
            {
                if ((sb.Length > 0))
                {
                    sb.Append(" ");
                }
                parseBestOutPutWithEachCutomer(inputModel.CustomerInputLines);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Parse each customer Color Lines through the PossibleOutColor and into list of possible outs
        /// </summary>
        /// <param name="customerColorLine"></param>
        private void parseBestOutPutWithEachCutomer(IList<CustomerInputLine> customerColorLines)
        {
            listofPossibleOutPuts = new List<OutPutModel>();
            foreach (var possibleOutPutColor in possibleCombinationOfColors)
            {
                //if the possibleOutPutColor Match with Customer color than add into list possible OutputModel
                if (isPossibleOutPutMatchWithCustomerColor(customerColorLines, possibleOutPutColor))
                {
                    OutPutModel outPutModel = new OutPutModel { PossibleOutPut = possibleOutPutColor };
                    listofPossibleOutPuts.Add(outPutModel);
                }

            }
        }

        /// <summary>
        /// Create string on the bases of customer color lines for example
        /// if the customer this color 1 M 3 G 5 G the following function will generate this string M%G%G and check with possible color output with this string
        /// </summary>
        /// <param name="customerColorLines"></param>
        /// <param name="possibleOutPutColor"></param>
        /// <returns></returns>
        private bool isPossibleOutPutMatchWithCustomerColor(IList<CustomerInputLine> customerColorLines, string possibleOutPutColor)
        {
            customerString = new List<string>();
            foreach (var customerColors in customerColorLines)
            {
                string secString = string.Empty;
                for (int i = 1; i <= inputModel.OutPutColorCount; i++)
                {
                    CustomerModel customerModel = customerColors.CustomerColorLine.SingleOrDefault(t => t.Paint == i);
                    if (customerModel != null)
                    {
                        secString += customerModel.ColorFinish.GetAbbreviation();
                    }
                    else
                    {
                        secString += "%";
                    }
                }
                customerString.Add(secString);
            }

            foreach (var customerstr in customerString)
            {
                IEnumerable<bool> z = commonChars(possibleOutPutColor, customerstr);
                if (!z.Any(r => r == true))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Generete all possible of G M on the bases first line of input
        /// </summary>
        /// <param name="outPutColorColount"></param>
        private void generatePossibleCombination(int outPutColorCount)
        {
            var alphabet = $"{ColorEnums.ColorFinish.GLOSS.GetAbbreviation()}{ColorEnums.ColorFinish.MATTE.GetAbbreviation()}";
            possibleCombinationOfColors = alphabet.Select(x => x.ToString());
            int size = outPutColorCount;
            for (int i = 0; i < size - 1; i++)
                possibleCombinationOfColors = possibleCombinationOfColors.SelectMany(x => alphabet, (x, y) => x + y);

        }

        /// <summary>
        /// take most less expensive out put
        /// </summary>
        /// <returns></returns>
        private string takeLessExpensiveOutPut()
        {
            if (listofPossibleOutPuts == null || listofPossibleOutPuts.Count == 0)
                return Errors.Nosolutionexists.GetDescription();

            return listofPossibleOutPuts.OrderByDescending(z => z.CountG).FirstOrDefault().PossibleOutPut;
        }

        /// <summary>
        /// Match the customer color string with possbleOutPutList
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private IEnumerable<bool> commonChars(string possbleOutPutColor, string customerColor)
        {
            if (possbleOutPutColor == null)
                throw new ArgumentNullException("possbleOutPutColor");

            if (customerColor == null)
                throw new ArgumentNullException("customerColor");

            var charsToCompare = possbleOutPutColor.Zip(customerColor, (LeftChar, RightChar) => new { LeftChar, RightChar });
            var matchingChars = charsToCompare.Select(pair => pair.LeftChar == pair.RightChar);

            return matchingChars;
        }

        #endregion
    }
}
