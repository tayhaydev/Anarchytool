using System.Net;

namespace Random_Unicode
{
    partial class Program
    {
        class Http
                        {
                            public static byte[] Post(string uri, NameValueCollection pairs)
                            {
                                using (WebClient webClient = new WebClient())
                                    return webClient.UploadValues(uri, pairs);
                            }
                        }
                }
            }
        }
    }
}
