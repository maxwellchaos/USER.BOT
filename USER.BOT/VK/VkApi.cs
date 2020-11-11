using MethodButton.VK.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodButton.VK
{
    public class VkApi
    {
        public string AccessToken { get; set; }

        public Wall Wall { get; private set; }

        public async Task AuthAsync()
        {
            this.Wall = new Wall(AccessToken);
            await Task.CompletedTask;
        }

        public void Auth() => AuthAsync().GetAwaiter().GetResult();
    }
}
