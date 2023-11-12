namespace Calculatorr.Models
{
    public class MonthInfo
    {
        public double BodyDebt {  get; set; }
        public double Percent { get; set; }
        public double RemainingDebt {
            get => remainingDebt;
            set { if (value < 0.02) remainingDebt = 0;
                else remainingDebt = value;
            } }
        private double remainingDebt;
        public Dictionary<int,string> dict = new Dictionary<int, string> {
            {1,"Января" },
            {2, "Ферваля"},
            {3, "Марта"},
            {4,"Апреля" },
            {5,"Мая" },
            {6,"Июня" },
            {7,"Июля" },
            {8,"Августа" },
            {9, "Сентября"},
            {10,"Октября" },
            {11, "Ноября"},
            {12, "Декабря"},
        };
}
}
