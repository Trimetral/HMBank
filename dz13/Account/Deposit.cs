using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz13.Account
{
    /// <summary>
    /// Вклад и связанная с ним логика
    /// </summary>
    public class Deposit
    {
        decimal amount;

        /// <summary>
        /// Сумма вклада
        /// </summary>
        public decimal Amount 
        { 
            get => amount;
            set 
            {
                amount = value;
                //DepositChanged?.Invoke();
            } 
        }

        /// <summary>
        /// Ставка
        /// </summary>
        public double Percent { get; set; }

        /// <summary>
        /// С капитализацией ли вклад
        /// </summary>
        public bool IsCapitilised { get; set; }

        //public event Action DepositChanged;

        /// <summary>
        /// Вклад и связанная с ним логика
        /// </summary>
        public Deposit()
        {
            Amount = 0;
            Percent = 0;
            IsCapitilised = false;
        }

        /// <summary>
        /// Рассчитать доход от вклада на определённый промежуток времени
        /// </summary>
        /// <returns>Доходы в виде листа строк</returns>
        public List<string> Calculate(int duration)
        {
            List<string> data = new List<string>();

            decimal temp = this.Amount;
            for(int i = 1; i <= duration; i++)
            {
                if (IsCapitilised)
                {
                    temp += temp * (decimal)this.Percent / 100 / 12;
                }
                else
                {
                    temp += temp * (decimal)this.Percent / 100;
                }
                data.Add($"{i}) {temp.ToString("0.##")}");
            }

            return data;
        }

        public override string ToString() => amount.ToString("#.##");
    }
}
