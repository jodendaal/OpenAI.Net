using System.Net;
using OpenAI.Net.Services;

namespace OpenAI.Net.Tests.Services.ModelsService_Tests
{
    internal class ModelService_GetById : BaseServiceTest
    {
        const string responseJson = @"{
                                            ""id"": ""babbage"",
                                            ""object"": ""model"",
                                            ""created"": 1649358449,
                                            ""owned_by"": ""openai"",
                                            ""permission"": [
                                                {
                                                    ""id"": ""modelperm-49FUp5v084tBB49tC4z8LPH5"",
                                                    ""object"": ""model_permission"",
                                                    ""created"": 1669085501,
                                                    ""allow_create_engine"": false,
                                                    ""allow_sampling"": true,
                                                    ""allow_logprobs"": true,
                                                    ""allow_search_indices"": false,
                                                    ""allow_view"": true,
                                                    ""allow_fine_tuning"": false,
                                                    ""organization"": ""*"",
                                                    ""group"": null,
                                                    ""is_blocking"": false
                                                }
                                            ],
                                            ""root"": ""babbage"",
                                            ""parent"": null
                                        }
            ";



        [TestCase(true, HttpStatusCode.OK, responseJson, null, Description = "Successfull Request", TestName = "GetById_When_Success")]
        [TestCase(false, HttpStatusCode.BadRequest, ErrorResponseJson, "an error occured", Description = "Failed Request", TestName = "GetById_When_Fail")]
        public async Task GetById(bool isSuccess, HttpStatusCode responseStatusCode, string responseJson, string errorMessage)
        {
            var httpClient = GetHttpClient(responseStatusCode, responseJson, "/v1/models/babbage");

            var service = new ModelsService(httpClient);
            var response = await service.Get("babbage");

            Assert.That(response.Result?.Id == "babbage", Is.EqualTo(isSuccess));

            AssertResponse(response,isSuccess,errorMessage, responseStatusCode);
        }
    }
}
