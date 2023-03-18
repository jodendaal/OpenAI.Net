using System.Text.Json.Serialization;
using System.Text.Json;

namespace OpenAI.Net
{
    public class ModelTypes
    {
        /// <summary>
        /// ada
        /// </summary>
        public static readonly ModelTypes Ada = new ModelTypes("ada");

        /// <summary>
        /// ada-code-search-code
        /// </summary>
        public static readonly ModelTypes AdaCodeSearchCode = new ModelTypes("ada-code-search-code");

        /// <summary>
        /// ada-code-search-text
        /// </summary>
        public static readonly ModelTypes AdaCodeSearchText = new ModelTypes("ada-code-search-text");

        /// <summary>
        /// ada-search-document
        /// </summary>
        public static readonly ModelTypes AdaSearchDocument = new ModelTypes("ada-search-document");

        /// <summary>
        /// ada-search-query
        /// </summary>
        public static readonly ModelTypes AdaSearchQuery = new ModelTypes("ada-search-query");

        /// <summary>
        /// ada-similarity
        /// </summary>
        public static readonly ModelTypes AdaSimilarity = new ModelTypes("ada-similarity");

        /// <summary>
        /// ada:2020-05-03
        /// </summary>
        public static readonly ModelTypes Ada_20200503 = new ModelTypes("ada:2020-05-03");

        /// <summary>
        /// babbage
        /// </summary>
        public static readonly ModelTypes Babbage = new ModelTypes("babbage");

        /// <summary>
        /// babbage-code-search-code
        /// </summary>
        public static readonly ModelTypes BabbageCodeSearchCode = new ModelTypes("babbage-code-search-code");

        /// <summary>
        /// babbage-code-search-text
        /// </summary>
        public static readonly ModelTypes BabbageCodeSearchText = new ModelTypes("babbage-code-search-text");

        /// <summary>
        /// babbage-search-document
        /// </summary>
        public static readonly ModelTypes BabbageSearchDocument = new ModelTypes("babbage-search-document");

        /// <summary>
        /// babbage-search-query
        /// </summary>
        public static readonly ModelTypes BabbageSearchQuery = new ModelTypes("babbage-search-query");

        /// <summary>
        /// babbage-similarity
        /// </summary>
        public static readonly ModelTypes BabbageSimilarity = new ModelTypes("babbage-similarity");

        /// <summary>
        /// babbage:2020-05-03
        /// </summary>
        public static readonly ModelTypes Babbage_20200503 = new ModelTypes("babbage:2020-05-03");

        /// <summary>
        /// code-cushman-001
        /// </summary>
        public static readonly ModelTypes CodeCushman001 = new ModelTypes("code-cushman-001");

        /// <summary>
        /// code-davinci-002
        /// </summary>
        public static readonly ModelTypes CodeDavinci002 = new ModelTypes("code-davinci-002");

        /// <summary>
        /// code-davinci-edit-001
        /// </summary>
        public static readonly ModelTypes CodeDavinciEdit001 = new ModelTypes("code-davinci-edit-001");

        /// <summary>
        /// code-search-ada-code-001
        /// </summary>
        public static readonly ModelTypes CodeSearchAdaCode001 = new ModelTypes("code-search-ada-code-001");

        /// <summary>
        /// code-search-ada-text-001
        /// </summary>
        public static readonly ModelTypes CodeSearchAdaText001 = new ModelTypes("code-search-ada-text-001");

        /// <summary>
        /// code-search-babbage-code-001
        /// </summary>
        public static readonly ModelTypes CodeSearchBabbageCode001 = new ModelTypes("code-search-babbage-code-001");

        /// <summary>
        /// code-search-babbage-text-001
        /// </summary>
        public static readonly ModelTypes CodeSearchBabbageText001 = new ModelTypes("code-search-babbage-text-001");

        /// <summary>
        /// curie
        /// </summary>
        public static readonly ModelTypes Curie = new ModelTypes("curie");

        /// <summary>
        /// curie-instruct-beta
        /// </summary>
        public static readonly ModelTypes CurieInstructBeta = new ModelTypes("curie-instruct-beta");

        /// <summary>
        /// curie-search-document
        /// </summary>
        public static readonly ModelTypes CurieSearchDocument = new ModelTypes("curie-search-document");

        /// <summary>
        /// curie-search-query
        /// </summary>
        public static readonly ModelTypes CurieSearchQuery = new ModelTypes("curie-search-query");

        /// <summary>
        /// curie-similarity
        /// </summary>
        public static readonly ModelTypes CurieSimilarity = new ModelTypes("curie-similarity");

        /// <summary>
        /// curie:2020-05-03
        /// </summary>
        public static readonly ModelTypes Curie_20200503 = new ModelTypes("curie:2020-05-03");

        /// <summary>
        /// cushman:2020-05-03
        /// </summary>
        public static readonly ModelTypes Cushman_20200503 = new ModelTypes("cushman:2020-05-03");

        /// <summary>
        /// davinci
        /// </summary>
        public static readonly ModelTypes Davinci = new ModelTypes("davinci");

        /// <summary>
        /// davinci-if:3.0.0
        /// </summary>
        public static readonly ModelTypes DavinciIf_3_0_0 = new ModelTypes("davinci-if:3.0.0");

        /// <summary>
        /// davinci-instruct-beta
        /// </summary>
        public static readonly ModelTypes DavinciInstructBeta = new ModelTypes("davinci-instruct-beta");

        /// <summary>
        /// davinci-instruct-beta:2.0.0
        /// </summary>
        public static readonly ModelTypes DavinciInstructBeta_2_0_0 = new ModelTypes("davinci-instruct-beta:2.0.0");

        /// <summary>
        /// davinci-search-document
        /// </summary>
        public static readonly ModelTypes DavinciSearchDocument = new ModelTypes("davinci-search-document");

        /// <summary>
        /// davinci-search-query
        /// </summary>
        public static readonly ModelTypes DavinciSearchQuery = new ModelTypes("davinci-search-query");

        /// <summary>
        /// davinci-similarity
        /// </summary>
        public static readonly ModelTypes DavinciSimilarity = new ModelTypes("davinci-similarity");

        /// <summary>
        /// davinci:2020-05-03
        /// </summary>
        public static readonly ModelTypes Davinci_20200503 = new ModelTypes("davinci:2020-05-03");

        /// <summary>
        /// gpt-3.5-turbo
        /// </summary>
        public static readonly ModelTypes GPT35Turbo = new ModelTypes("gpt-3.5-turbo");

        /// <summary>
        /// gpt-3.5-turbo-0301
        /// </summary>
        public static readonly ModelTypes GPT35Turbo0301 = new ModelTypes("gpt-3.5-turbo-0301");

        /// <summary>
        /// if-curie-v2
        /// </summary>
        public static readonly ModelTypes IfCurieV2 = new ModelTypes("if-curie-v2");

        /// <summary>
        /// if-davinci-v2
        /// </summary>
        public static readonly ModelTypes IfDavinciV2 = new ModelTypes("if-davinci-v2");

        /// <summary>
        /// if-davinci:3.0.0
        /// </summary>
        public static readonly ModelTypes IfDavinci_3_0_0 = new ModelTypes("if-davinci:3.0.0");

        /// <summary>
        /// text-ada-001
        /// </summary>
        public static readonly ModelTypes TextAda001 = new ModelTypes("text-ada-001");

        /// <summary>
        /// text-ada:001
        /// </summary>
        public static readonly ModelTypes TextAda_001 = new ModelTypes("text-ada:001");

        /// <summary>
        /// text-babbage-001
        /// </summary>
        public static readonly ModelTypes TextBabbage001 = new ModelTypes("text-babbage-001");

        /// <summary>
        /// text-babbage:001
        /// </summary>
        public static readonly ModelTypes TextBabbage_001 = new ModelTypes("text-babbage:001");

        /// <summary>
        /// text-curie-001
        /// </summary>
        public static readonly ModelTypes TextCurie001 = new ModelTypes("text-curie-001");

        /// <summary>
        /// text-curie:001
        /// </summary>
        public static readonly ModelTypes TextCurie_001 = new ModelTypes("text-curie:001");

        /// <summary>
        /// text-davinci-001
        /// </summary>
        public static readonly ModelTypes TextDavinci001 = new ModelTypes("text-davinci-001");

        /// <summary>
        /// text-davinci-002
        /// </summary>
        public static readonly ModelTypes TextDavinci002 = new ModelTypes("text-davinci-002");

        /// <summary>
        /// text-davinci-003
        /// </summary>
        public static readonly ModelTypes TextDavinci003 = new ModelTypes("text-davinci-003");

        /// <summary>
        /// text-davinci-edit-001
        /// </summary>
        public static readonly ModelTypes TextDavinciEdit001 = new ModelTypes("text-davinci-edit-001");

        /// <summary>
        /// text-davinci-insert-001
        /// </summary>
        public static readonly ModelTypes TextDavinciInsert001 = new ModelTypes("text-davinci-insert-001");

        /// <summary>
        /// text-davinci-insert-002
        /// </summary>
        public static readonly ModelTypes TextDavinciInsert002 = new ModelTypes("text-davinci-insert-002");

        /// <summary>
        /// text-davinci:001
        /// </summary>
        public static readonly ModelTypes TextDavinci_001 = new ModelTypes("text-davinci:001");

        /// <summary>
        /// text-embedding-ada-002
        /// </summary>
        public static readonly ModelTypes TextEmbeddingAda002 = new ModelTypes("text-embedding-ada-002");

        /// <summary>
        /// text-search-ada-doc-001
        /// </summary>
        public static readonly ModelTypes TextSearchAdaDoc001 = new ModelTypes("text-search-ada-doc-001");

        /// <summary>
        /// text-search-ada-query-001
        /// </summary>
        public static readonly ModelTypes TextSearchAdaQuery001 = new ModelTypes("text-search-ada-query-001");

        /// <summary>
        /// text-search-babbage-doc-001
        /// </summary>
        public static readonly ModelTypes TextSearchBabbageDoc001 = new ModelTypes("text-search-babbage-doc-001");

        /// <summary>
        /// text-search-babbage-query-001
        /// </summary>
        public static readonly ModelTypes TextSearchBabbageQuery001 = new ModelTypes("text-search-babbage-query-001");

        /// <summary>
        /// text-search-curie-doc-001
        /// </summary>
        public static readonly ModelTypes TextSearchCurieDoc001 = new ModelTypes("text-search-curie-doc-001");

        /// <summary>
        /// text-search-curie-query-001
        /// </summary>
        public static readonly ModelTypes TextSearchCurieQuery001 = new ModelTypes("text-search-curie-query-001");

        /// <summary>
        /// text-search-davinci-doc-001
        /// </summary>
        public static readonly ModelTypes TextSearchDavinciDoc001 = new ModelTypes("text-search-davinci-doc-001");

        /// <summary>
        /// text-search-davinci-query-001
        /// </summary>
        public static readonly ModelTypes TextSearchDavinciQuery001 = new ModelTypes("text-search-davinci-query-001");

        /// <summary>
        /// text-similarity-ada-001
        /// </summary>
        public static readonly ModelTypes TextSimilarityAda001 = new ModelTypes("text-similarity-ada-001");

        /// <summary>
        /// text-similarity-babbage-001
        /// </summary>
        public static readonly ModelTypes TextSimilarityBabbage001 = new ModelTypes("text-similarity-babbage-001");

        /// <summary>
        /// text-similarity-curie-001
        /// </summary>
        public static readonly ModelTypes TextSimilarityCurie001 = new ModelTypes("text-similarity-curie-001");

        /// <summary>
        /// text-similarity-davinci-001
        /// </summary>
        public static readonly ModelTypes TextSimilarityDavinci001 = new ModelTypes("text-similarity-davinci-001");

        /// <summary>
        /// whisper-1
        /// </summary>
        public static readonly ModelTypes Whisper1 = new ModelTypes("whisper-1");

        private readonly string _value;

        private ModelTypes(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }

        public static implicit operator string(ModelTypes action)
        {
            return action._value;
        }

        public static implicit operator ModelTypes(string modelName)
        {
            return new ModelTypes(modelName);
        }

        public class ModelTypesJsonConverter : JsonConverter<ModelTypes>
        {
            public override ModelTypes? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return new ModelTypes(reader.GetString()!);
            }

            public override void Write(Utf8JsonWriter writer, ModelTypes value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }

    }

    //public static class ModelTypes
    //{
    //    public const string Ada = "ada";
    //    public const string AdaCodeSearchCode = "ada-code-search-code";
    //    public const string AdaCodeSearchText = "ada-code-search-text";
    //    public const string AdaSearchDocument = "ada-search-document";
    //    public const string AdaSearchQuery = "ada-search-query";
    //    public const string AdaSimilarity = "ada-similarity";
    //    public const string Ada_20200503 = "ada:2020-05-03";
    //    public const string AudioTranscribe001 = "audio-transcribe-001";
    //    public const string Babbage = "babbage";
    //    public const string BabbageCodeSearchCode = "babbage-code-search-code";
    //    public const string BabbageCodeSearchText = "babbage-code-search-text";
    //    public const string BabbageSearchDocument = "babbage-search-document";
    //    public const string BabbageSearchQuery = "babbage-search-query";
    //    public const string BabbageSimilarity = "babbage-similarity";
    //    public const string Babbage_20200503 = "babbage:2020-05-03";
    //    public const string CodeCushman001 = "code-cushman-001";
    //    public const string CodeDavinci002 = "code-davinci-002";
    //    public const string CodeDavinciEdit001 = "code-davinci-edit-001";
    //    public const string CodeSearchAdaCode001 = "code-search-ada-code-001";
    //    public const string CodeSearchAdaText001 = "code-search-ada-text-001";
    //    public const string CodeSearchBabbageCode001 = "code-search-babbage-code-001";
    //    public const string CodeSearchBabbageText001 = "code-search-babbage-text-001";
    //    public const string Curie = "curie";
    //    public const string CurieInstructBeta = "curie-instruct-beta";
    //    public const string CurieSearchDocument = "curie-search-document";
    //    public const string CurieSearchQuery = "curie-search-query";
    //    public const string CurieSimilarity = "curie-similarity";
    //    public const string Curie_20200503 = "curie:2020-05-03";
    //    public const string Cushman_20200503 = "cushman:2020-05-03";
    //    public const string Davinci = "davinci";
    //    public const string DavinciIf_3_0_0 = "davinci-if:3.0.0";
    //    public const string DavinciInstructBeta = "davinci-instruct-beta";
    //    public const string DavinciInstructBeta_2_0_0 = "davinci-instruct-beta:2.0.0";
    //    public const string DavinciSearchDocument = "davinci-search-document";
    //    public const string DavinciSearchQuery = "davinci-search-query";
    //    public const string DavinciSimilarity = "davinci-similarity";
    //    public const string Davinci_20200503 = "davinci:2020-05-03";
    //    public const string IfCurieV2 = "if-curie-v2";
    //    public const string IfDavinciV2 = "if-davinci-v2";
    //    public const string IfDavinci_3_0_0 = "if-davinci:3.0.0";
    //    public const string TextAda001 = "text-ada-001";
    //    public const string TextAda_001 = "text-ada:001";
    //    public const string TextBabbage001 = "text-babbage-001";
    //    public const string TextBabbage_001 = "text-babbage:001";
    //    public const string TextCurie001 = "text-curie-001";
    //    public const string TextCurie_001 = "text-curie:001";
    //    public const string TextDavinci001 = "text-davinci-001";
    //    public const string TextDavinci002 = "text-davinci-002";
    //    public const string TextDavinci003 = "text-davinci-003";
    //    public const string TextDavinciEdit001 = "text-davinci-edit-001";
    //    public const string TextDavinciInsert001 = "text-davinci-insert-001";
    //    public const string TextDavinciInsert002 = "text-davinci-insert-002";
    //    public const string TextDavinci_001 = "text-davinci:001";
    //    public const string TextEmbeddingAda002 = "text-embedding-ada-002";
    //    public const string TextSearchAdaDoc001 = "text-search-ada-doc-001";
    //    public const string TextSearchAdaQuery001 = "text-search-ada-query-001";
    //    public const string TextSearchBabbageDoc001 = "text-search-babbage-doc-001";
    //    public const string TextSearchBabbageQuery001 = "text-search-babbage-query-001";
    //    public const string TextSearchCurieDoc001 = "text-search-curie-doc-001";
    //    public const string TextSearchCurieQuery001 = "text-search-curie-query-001";
    //    public const string TextSearchDavinciDoc001 = "text-search-davinci-doc-001";
    //    public const string TextSearchDavinciQuery001 = "text-search-davinci-query-001";
    //    public const string TextSimilarityAda001 = "text-similarity-ada-001";
    //    public const string TextSimilarityBabbage001 = "text-similarity-babbage-001";
    //    public const string TextSimilarityCurie001 = "text-similarity-curie-001";
    //    public const string TextSimilarityDavinci001 = "text-similarity-davinci-001";
    //    public const string GPT35Turbo = "gpt-3.5-turbo";
    //}
}
