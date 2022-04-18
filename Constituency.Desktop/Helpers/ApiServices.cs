using Constituency.Desktop.Entities;
using Constituency.Desktop.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace Constituency.Desktop.Helpers
{
    class ApiServices
    {
        public static async Task<Response> LoginAsync(LoginRequest model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                HttpResponseMessage response = await client.PostAsync("api/Account/CreateToken", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault()
                    };
                }
                TokenResponse tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = tokenResponse,
                };

            }


            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public static async Task<Response> GetListAsync<T>(string controller)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                HttpResponseMessage response = await client.GetAsync($"api/{controller}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault()
                    };
                }
                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        //recover password
        public static async Task<Response> PostAsync<T>(string controller, T model)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                HttpResponseMessage response = await client.PostAsync($"api/{controller}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                //T item = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }


        }
        public static async Task<Response> PutAsync<T>(string controller, T model, int id)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                HttpResponseMessage response = await client.PutAsync($"api/{controller}/{id}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                T item = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = item,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> DeleteAsync(string controller, int id)
        {
            try
            {

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                HttpResponseMessage response = await client.DeleteAsync($"api/{controller}/{id}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }

                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> GetListAsync<T>(string controller, string token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.GetAsync($"api/{controller}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }


        }
        public static async Task<Response> GetListAsync<T>(string controller, string statuses, string token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)


                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.GetAsync($"api/{controller}/{statuses}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }


        }
        public static async Task<Response> PostAsync<T>(string controller, T model, string token)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.PostAsync($"api/{controller}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                T item = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = item,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> PutAsync<T>(string controller, T model, int id, string token)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.PutAsync($"api/{controller}/{id}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                T item = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = item,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> DeleteAsync(string controller, int id, string token)
        {
            try
            {

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.DeleteAsync($"api/{controller}/{id}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }

                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> DeleteAsync(string controller, string id, string token)
        {
            try
            {

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.DeleteAsync($"api/{controller}/{id}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }

                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        //personalizados
        public static async Task<Response> FindAsync<T>(string controller, string id, string token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                string address;

                address = $"api/{controller}/{id}";

                HttpResponseMessage response = await client.GetAsync(address);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "Not Found",
                        };
                    }
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                var element = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = element,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }


        }
        public static async Task<Response> RemoveJointAsync<T>(string controller, int id, int applicantId, T model, string token)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.PutAsync($"api/{controller}/{id},{applicantId}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                T item = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = item,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> GetListAsyncReports<T>(string controller, string fromD, string toD, string indicator, string token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)


                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.GetAsync($"api/{controller}/{fromD}/{toD}/{indicator}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                List<T> list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }


        }
        public static async Task<Response> GetUserRoles<T>(string controller, string userName, string token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)


                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.GetAsync($"api/{controller}/{userName}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                List<string> list = JsonConvert.DeserializeObject<List<string>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }


        }
        public static async Task<Response> AddUserAccess<T>(string controller, T model, string access, string token)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.PutAsync($"api/{controller}/{access}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                T item = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = item,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> RegisterUser(string controller, AddUserViewModel model, string token)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.PostAsync($"api/{controller}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = !string.IsNullOrEmpty(result)?JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault():"Controller not found"
                    };
                }
                User item = JsonConvert.DeserializeObject<User>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = item,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> ResendEmail(string controller, string username, string token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.GetAsync($"api/{controller}/{ username}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "Confirmation Email successfully sent"
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public static async Task<Response> LogutAsync(string controller, string token)
        {
            try
            {
                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.DeleteAsync($"api/{controller}");
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }

                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public static async Task<Response> PostMasterFileAsync<T>(string controller, T model, string token)
        {
            try
            {
                string request = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(request, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

                string url = Settings.GetApiUrl();
                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(url)
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                HttpResponseMessage response = await client.PostAsync($"api/{controller}", content);
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
                    };
                }
                // T item = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = "done",
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        //public static async Task<Response> GetListAsyncMasterFile<T>(string controller, string token)
        //{
        //    try
        //    {
        //        HttpClientHandler handler = new HttpClientHandler()
        //        {
        //            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        //        };

        //        string url = Settings.GetApiUrl();
        //        HttpClient client = new HttpClient(handler)
        //        {
        //            BaseAddress = new Uri(url)
        //        };
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        //        HttpResponseMessage response = await client.GetAsync($"api/{controller}");
        //        string result = await response.Content.ReadAsStringAsync();
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return new Response
        //            {
        //                IsSuccess = false,
        //                Message = JsonConvert.DeserializeObject<ErrorMessage>(result).Error.FirstOrDefault(),
        //            };
        //        }
        //        List<MasterFile> list = JsonConvert.DeserializeObject<List<MasterFile>>(result);
        //        return new Response
        //        {
        //            IsSuccess = true,
        //            Result = list,
        //        };

        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message
        //        };
        //    }


        //}
    }
}
