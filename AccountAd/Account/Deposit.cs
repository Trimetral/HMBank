using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountAdLibrary.Account
{
    /// <summary>
    /// Вклад и связанная с ним логика
    /// </summary>
    public class Deposit
    {
        /// <summary>
        /// Сумма вклада
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Ставка
        /// </summary>
        public double Percent { get; set; }

        /// <summary>
        /// С капитализацией ли вклад
        /// </summary>
        public bool IsCapitilised { get; set; }

        /// <summary>
        /// Был ли вклад открыт
        /// </summary>
        public bool IsOpend { get; set; } = false;

        /// <summary>
        /// Вклад и связанная с ним логика
        /// </summary>
        public Deposit()
        {
            (Amount, Percent, IsCapitilised, IsOpend) = (0, 0, false, false);
        }

        /// <summary>
        /// Рассчитать доход от вклада на определённый промежуток времени
        /// </summary>
        /// <returns>Доходы в виде листа строк</returns>
        public IEnumerable<string> Calculate(int duration)
        {
            List<string> data = new List<string>();

            decimal temp = Amount;
            for(int i = 1; i <= duration; i++)
            {
                if (IsCapitilised)
                {
                    temp += temp * (decimal)Percent / 100 / 12;
                }
                else
                {
                    temp += temp * (decimal)Percent / 100;
                }
                data.Add($"{i}) {temp:0.##}");
            }

            return data;
        }

        public override string ToString() => Amount.ToString("#.##");
    }
}
