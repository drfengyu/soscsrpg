using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://postapi.drfengling.online");

        //webRequest.Method = "GET";

        //webRequest.ContentType = "application/json";

        //webRequest.Accept = "application/json";

        //byte[] byteArray = new byte[100];
        //using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
        //{
        //     var stream = response.GetResponseStream();
        //    stream.ReadAsync(byteArray, 0, byteArray.Length);
        //    Console.WriteLine(Encoding.UTF8.GetString(byteArray));
        //    Console.WriteLine(response.StatusCode);
        //    // 
        //}
        //Console.ReadKey();
    //}

        static readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            try
            {
                // 替换为你要发送的API URL
                string url = "https://postapi.drfengling.online";

                // 创建要发送的数据
                //var content = new StringContent("{\"key1\":\"value1\", \"key2\":\"value2\"}", Encoding.UTF8, "application/json");
                //HttpResponseMessage response = await client.PostAsync(url, content);
                // 发送POST请求
                HttpResponseMessage response = await client.GetAsync(url);

                // 检查响应状态
                if (response.IsSuccessStatusCode)
                {
                    // 读取响应内容
                    //string responseBody = await response.Content.ReadAsStringAsync();
                    string responseBody=await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {responseBody}");
                }
                else
                {
                    // 输出错误信息
                    Console.WriteLine($"Error: {response.ReasonPhrase}");
                }
                Console.ReadKey();
            }
            catch (HttpRequestException e)
            {
                // 处理异常
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    
    }
}
