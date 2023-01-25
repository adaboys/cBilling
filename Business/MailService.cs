namespace App;

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

public class MailService {
	private readonly AppSetting appSetting;

	public MailService(IOptions<AppSetting> appSettingOpt) {
		this.appSetting = appSettingOpt.Value;
	}

	/// Ref: https://docs.aws.amazon.com/ses/latest/dg/send-using-smtp-programmatically.html
	public async Task<ApiResponse> Send(string toEmail, string subject, string body, bool isBodyHtml = false) {
		var mailSetting = this.appSetting.mail;

		// If you're using Amazon SES in a region other than US West (Oregon),
		// replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP
		// endpoint in the appropriate AWS Region.
		var host = $"email-smtp.{mailSetting.region}.amazonaws.com";

		// The port you will connect to on the Amazon SES SMTP endpoint.
		// We are choosing port 587 because we will use STARTTLS to encrypt the connection.
		var port = 587;

		// Create and build a new MailMessage object
		var message = new MailMessage();
		message.IsBodyHtml = isBodyHtml;
		message.From = new MailAddress(mailSetting.fromEmail, mailSetting.fromName);
		message.To.Add(new MailAddress(toEmail));
		message.Subject = subject;
		message.Body = body;

		using (var client = new System.Net.Mail.SmtpClient(host, port)) {
			try {
				// Setup client
				client.Credentials = new NetworkCredential(mailSetting.smtpUsername, mailSetting.smtpPassword);
				client.EnableSsl = true;

				await client.SendMailAsync(message);

				return new ApiSuccessResponse("Email sent !");
			}
			catch (Exception e) {
				return new ApiInternalServerErrorResponse(this.appSetting.debug ? e.Message : "Could not send email");
			}
		}
	}
}
