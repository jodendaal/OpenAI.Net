using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAI.Net.Models.Responses
{
    public class CreateFineTuneResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public string Model { get; set; }
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }
        public Event[] Events { get; set; }
        [JsonPropertyName("fine_tuned_model")]
        public string FineTunedModel { get; set; }

        [JsonPropertyName("hyperparams")]
        public HyperParam HyperParams { get; set; }
        [JsonPropertyName("organization_id")]
        public string OrganizationId { get; set; }

        [JsonPropertyName("result_files")]
        public FileInfoResponse[] ResultFiles { get; set; }
        public string Status { get; set; }
        [JsonPropertyName("validation_files")]
        public FileInfoResponse[] ValidationFiles { get; set; }
        [JsonPropertyName("training_files")]
        public FileInfoResponse[] TrainingFiles { get; set; }
        [JsonPropertyName("updated_at")]
        public int UpdatedAt { get; set; }
    }

    public class HyperParam
    {
        [JsonPropertyName("batch_size")]
        public int BatchSize { get; set; }
        [JsonPropertyName("learning_rate_multiplier")]
        public double LearningRateMultiplier { get; set; }
        [JsonPropertyName("n_epochs")]
        public int NoOfEpochs { get; set; }
        [JsonPropertyName("prompt_loss_weight")]
        public double PromptLossWeight { get; set; }
    }

    public class Event
    {
        public string Object { get; set; }
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }
}
