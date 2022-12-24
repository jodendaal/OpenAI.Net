using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenAI.Net.Models.Requests
{
    public class FineTuneRequest
    {
        public FineTuneRequest(string trainingFile)
        {
            TrainingFile = trainingFile;
        }

        /// <summary>
        /// The ID of an uploaded file that contains training data. <br />
        /// See <a href="https://beta.openai.com/docs/api-reference/files/upload">upload file</a> for how to upload a file. <br />
        /// Your dataset must be formatted as a JSONL file, where each training example is a JSON object with the keys "prompt" and "completion". <br />
        /// Additionally, you must upload your file with the purpose fine-tune. <br />
        /// See the <a href="https://beta.openai.com/docs/guides/fine-tuning/creating-training-data">fine-tuning</a> guide for more details. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-training_file" />
        /// </summary>
        [JsonPropertyName("training_file")]
        [Required]
        public string TrainingFile { get; set; }

        /// <summary>
        /// The ID of an uploaded file that contains validation data. <br />
        /// If you provide this file, the data is used to generate validation metrics periodically during fine-tuning. <br />
        /// These metrics can be viewed in the <a href="https://beta.openai.com/docs/guides/fine-tuning/analyzing-your-fine-tuned-model">fine-tuning results file</a> . <br />
        /// Your train and validation data should be mutually exclusive. <br />
        /// Your dataset must be formatted as a JSONL file, where each training example is a JSON object with the keys "prompt" and "completion". <br />
        /// Additionally, you must upload your file with the purpose fine-tune. <br />
        /// See the <a href="https://beta.openai.com/docs/guides/fine-tuning/creating-training-data">fine-tuning</a> guide for more details. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-validation_file" />
        /// </summary>
        [JsonPropertyName("validation_file")]
        public string ValidationFile { get; set; }

        /// <summary>
        /// The name of the base model to fine-tune.You can select one of "ada", "babbage", "curie", "davinci", or a fine-tuned model created after 2022-04-21.  <br />
        /// To learn more about these models, see the <a href="https://beta.openai.com/docs/models">Models</a> documentation. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-model" />
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The number of epochs to train the model for. <br />
        /// An epoch refers to one full cycle through the training dataset. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-n_epochs" />
        /// </summary>
        [JsonPropertyName("n_epochs")]
        public int? NoOfEpochs { get; set; }

        /// <summary>
        /// The batch size to use for training. <br />
        /// The batch size is the number of training examples used to train a single forward and backward pass. <br />
        /// By default, the batch size will be dynamically configured to be ~0.2% of the number of examples in the training set, capped at 256 - in general, we've found that larger batch sizes tend to work better for larger datasets. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-batch_size" />
        /// </summary>
        [JsonPropertyName("batch_size")]
        public int? BatchSize { get; set; }

        /// <summary>
        /// The learning rate multiplier to use for training. <br />
        /// The fine-tuning learning rate is the original learning rate used for pretraining multiplied by this value. <br />
        /// By default, the learning rate multiplier is the 0.05, 0.1, or 0.2 depending on final batch_size (larger learning rates tend to perform better with larger batch sizes). We recommend experimenting with values in the range 0.02 to 0.2 to see what produces the best results. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-batch_size" />
        /// </summary>
        [JsonPropertyName("learning_rate_multiplier")]
        public double? LearningRateMultiplier { get; set; }

        /// <summary>
        /// The weight to use for loss on the prompt tokens. <br />
        /// This controls how much the model tries to learn to generate the prompt (as compared to the completion which always has a weight of 1.0), and can add a stabilizing effect to training when completions are short. <br />
        /// If prompts are extremely long (relative to completions), it may make sense to reduce this weight so as to avoid over-prioritizing learning the prompt. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-prompt_loss_weight" />
        /// </summary>
        [JsonPropertyName("prompt_loss_weight")]
        public double? PromptLossWeight { get; set; }

        /// <summary>
        /// If set, we calculate classification-specific metrics such as accuracy and F-1 score using the validation set at the end of every epoch. These metrics can be viewed in the <a href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-compute_classification_metrics">results file</a>. <br />
        /// In order to compute classification metrics, you must provide a validation_file. <br />
        /// Additionally, you must specify classification_n_classes for multiclass classification or classification_positive_class for binary classification <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-classification_n_classes" />
        /// </summary>
        [JsonPropertyName("compute_classification_metrics")]
        public int? ComputeClassificationMetrics { get; set; }

        /// <summary>
        /// The number of classes in a classification task. <br />
        /// This parameter is required for multiclass classification. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-classification_n_classes" />
        /// </summary>
        [JsonPropertyName("classification_n_classes")]
        public int? ClassificationNoOfClasses { get; set; }

        /// <summary>
        /// The positive class in binary classification. <br />
        /// This parameter is needed to generate precision, recall, and F1 metrics when doing binary classification. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-classification_positive_class" />
        /// </summary>
        [JsonPropertyName("classification_positive_class")]
        public string ClassificationPositiveClass { get; set; }

        /// <summary>
        /// If this is provided, we calculate F-beta scores at the specified beta values.  <br />
        /// The F-beta score is a generalization of F-1 score. This is only used for binary classification. <br />
        /// With a beta of 1 (i.e. the F-1 score), precision and recall are given the same weight. <br />
        /// A larger beta score puts more weight on recall and less on precision.  <br />
        /// A smaller beta score puts more weight on precision and less on recall. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-classification_betas" />
        /// </summary>
        [JsonPropertyName("classification_betas")]
        public string ClassificationBetas { get; set; }

        /// <summary>
        /// A string of up to 40 characters that will be added to your fine-tuned model name.  <br />
        /// For example, a suffix of "custom-model-name" would produce a model name like
        /// <see href="https://beta.openai.com/docs/api-reference/fine-tunes/create#fine-tunes/create-suffix" />
        /// </summary>
        public string Suffix { get; set; }
    }
}
