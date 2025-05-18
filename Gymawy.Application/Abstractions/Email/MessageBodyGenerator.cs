using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymawy.Application.Abstractions.Email
{
    public static class MessageBodyGenerator
    {
        public static string GenerateEmailMessageStructure(string title, string code, string time)
        {
            return $@"
    <html>
        <body style='font-family: Arial, sans-serif; color: #333; line-height: 1.6;'>
            <p>Hello,</p>
            <p>Use the following code to <strong>{title}</strong>. This code is only valid for the next 
                <span style='font-weight: bold; color: #d9534f;'>{time} minutes</span>:</p>
            <div style='
                display: inline-block;
                padding: 15px 20px;
                margin: 15px 0;
                background-color: #f9f9f9;
                border: 1px solid #ddd;
                border-radius: 8px;
                box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
                font-size: 20px;
                color: #4CAF50; /* Green color for the code */
                font-weight: bold;
                text-align: center;'>
                {code}
            </div>
            <p>If you didn’t request this, you can safely ignore this email.</p>
            <p style='color:#000000; font-size: 16px;'>Best regards</p>
            <p style='color: #000; font-style: italic; font-weight: bold; font-size: 18px;'>Gymawy Team</p>
        </body>
    </html>";

        }
    }
}
