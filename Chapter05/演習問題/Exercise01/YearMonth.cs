using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    //5.1.1
    public record YearMonth(int Year, int Month) {
        
        //5.1.2
        public bool Is21Century => 2001 <= Year && Year <= 2100;
        /*public bool Is21Century {
         * get { return Year >= 2001 && Year <= 2100; }
         * } 
        */
        //5.1.3
        public YearMonth AddOneMonth() {
            int newYear = Year;
            int newMonth = Month + 1;
            if (newMonth > 12) {
                newYear++;
                newMonth = 1;
            }
            return new YearMonth(newYear, newMonth);
        }

        //5.1.4
        public override string ToString() => $"{Year}年{Month:00}月";

    }
}
