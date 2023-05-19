using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPP;

namespace capp_tests
{
    public class MainBlock
    {
        [Fact]
        public async Task BaseExistence()
        {
            var Database = new ProductDatabase();
            int count = 0;

            count = (await Database.ListItemAsync()).Count;

            Assert.NotEqual(0, count);
            Assert.True(count > 0);
        }
    }
}
