using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Abstractions.Stripe
{
    public class StripeOptions
    {
        public string PublishedKey { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;

        public string WebhooksSecretKey { get; set; } = string.Empty;

        public string SuccessUrl { get; set; } = string.Empty;

        public string CancelUrl { get; set; } = string.Empty;
        public string RefreshUrl { get; set; } = string.Empty;
       
        public string ReturnUrl { get; set; } = string.Empty;

    }
}
