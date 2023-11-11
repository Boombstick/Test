using Calculatorr.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace Calculatorr.Controllers
{
    public class HomeController : Controller
    {
        private double _monthlyPay = 0;
        //private double _stepPay = 0;
        private double _totalPercent = 0;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowResult(DataModel dataModel)
        {
            var loanAmount = dataModel.LoanAmount;
            var loanTerm = dataModel.LoanTerm;
            var loanRate = dataModel.LoanRate;

            _monthlyPay = StepPayCalculation(loanAmount, loanTerm, loanRate);
            var list = MainDebt(loanAmount, loanTerm, loanRate);

            var ViewModel = new ViewModel(loanAmount, loanTerm, loanRate, _monthlyPay, DateTime.Now, list);

            ViewData["TotalPercent"] = Math.Round(_totalPercent, 2);
            ViewData["TotalSum"] = Math.Round(_totalPercent + loanAmount, 2);

            return View(ViewModel);


        }
        [HttpGet]
        public IActionResult CalculatePerMonth()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CalculatePerMonth(DataModel dataModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("ShowResult", dataModel);
            }
            return Content("Введены некоректные данные");
        }
        [HttpGet]
        public IActionResult CalculatePerDay()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult CalculatePerDay(double loanAmount, int loanTerm, double loanRate, int step)
        //{
        //    //_stepPay = StepPayCalculation(loanAmount, loanTerm, loanRate, true, step);

            
        //}
        private double StepPayCalculation(double loanAmount, int loanTerm, double loanRate, bool PerDay = false, int step = 0)
        {
            double result = 0;
            //if (PerDay)
            //{
            //    double percent = 0;
            //    double i = loanRate / 100;         //дневная процентная ставка
            //    double debt = loanAmount;

            //    for (int j = 0; j <= loanTerm; j++)
            //    {
            //        percent += (debt + percent) * i;
            //        if (j == step)
            //        {
            //            debt = (debt + percent)/step;
            //            step += step;
            //        }

            //    }
            //    result = debt / step;
            //}

            double i = loanRate / 100 / 12;         //месячная процентная ставка              
            double numerator = i * Math.Pow(1 + i, loanTerm);
            double denumarator = Math.Pow(1 + i, loanTerm) - 1;
            double annuityRatio = numerator / denumarator;
            result = loanAmount * annuityRatio;

            return Math.Round(result, 2);
        }
        private double PercentCalculation(double loanAmount, double loanRate)
        {
            return loanAmount * loanRate / 100 / 12;

        }
        private List<MonthInfo> MainDebt(double loanAmount, int loanTerm, double loanRate)
        {
            var temp = loanAmount;
            var result = new List<MonthInfo>();
            double bodyDebt;
            double percent;
            double remainingDebt;

            for (int i = 0; i < loanTerm; i++)
            {
                percent = PercentCalculation(temp, loanRate);
                bodyDebt = _monthlyPay - percent;
                remainingDebt = temp - (_monthlyPay - percent);

                temp = temp - (_monthlyPay - percent);
                _totalPercent += percent;
                result.Add(new MonthInfo { BodyDebt = double.Round(bodyDebt, 2), Percent = double.Round(percent, 2), RemainingDebt = double.Round(remainingDebt, 2) });
            }

            return result;
        }
    }
}