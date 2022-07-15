using iot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot.Domain.ValueObjects
{
    public class Concurrency : ValueObject
    {
        #region constructure
        public Concurrency()
        {

        }
        #endregion

        public static string Value { get; set; }
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789qwertyuiopasdfghjklzxcvbnm";

        public static string NewToken()
        {
            var random = new Random();

            Value= new string(Enumerable.Repeat(chars, 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
