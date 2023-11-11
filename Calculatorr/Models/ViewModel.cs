using System.ComponentModel.DataAnnotations;

namespace Calculatorr.Models
{
    public class ViewModel
    {
        public List<MonthInfo> MonthsInfo {get; set;}
        public double LoanAmount {  get; set; }
        public int LoanTerm {  get; set; }
        public double LoanRate { get; set; }

        public double MonthlyPayment {  get; set; }
        public int Months { get; set; }
        public ViewModel(double loanAmount, int loanTerm, double loanRate, double monthlyPayment,DateTime month, List<MonthInfo> info)
        {
            LoanAmount = loanAmount;
            LoanTerm = loanTerm;
            LoanRate = loanRate;
            MonthlyPayment = monthlyPayment;
            Months = month.Month;
            MonthsInfo = info;

            
        }
    }
}
