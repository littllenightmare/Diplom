using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using HomeMenu.Database;
using System.Threading.Tasks;

namespace HomeMenu.Functions
{
    public class Authorization
    {
        /// <summary>
        /// Регистрация пользователя, инициализация письма с кодом и отправка на почту
        /// </summary>
        /// <param name="email">почтовый адрес</param>
        /// <returns>ничего(</returns>
        public async static Task SendEmail(string email)
        {
            HomeMenuContext context = new();
            try
            {
                var emailSettings = Privacy.ConfigurationHelper.GetEmailSettings();
                MailAddress fromAddress = new(emailSettings.Email, "Диплом");
                MailAddress toAddress = new(email);
                MailMessage mail = new(fromAddress, toAddress);

                var rnd = new Random();
                var code = rnd.Next(100000, 999999);

                var user = new User
                {
                    Email = email,
                    Code = code,
                    Role = 1
                };
                context.Users.Add(user);
                var profile = new Profile()
                {
                    UserId = user.Id,
                    User = user
                };
                context.Profiles.Add(profile);
                await context.SaveChangesAsync();

                Data.email = email;

                mail.Body = string.Format(@"
                <!DOCTYPE html>
                <html lang=""ru"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Код подтверждения</title>
                </head>
                <body style=""font-family: 'Arial', sans-serif; margin: 0; padding: 0; background-color: #f5f9ff;"">
                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""max-width: 600px; margin: 20px auto;"">
                        <tr>
                            <td style=""padding: 25px 20px; text-align: center; background-color: #a8d8ea; border-radius: 10px 10px 0 0;"">
                                <h1 style=""color: #2c3e50; margin: 0; font-size: 24px;"">Ваш код подтверждения</h1>
                            </td>
                        </tr>
                        <tr>
                            <td style=""padding: 30px 20px; background-color: #ffffff; border-left: 1px solid #e0e0e0; border-right: 1px solid #e0e0e0;"">
                                <p style=""color: #2c3e50; font-size: 16px; line-height: 1.5; margin-bottom: 25px;"">
                                    Спасибо за регистрацию! Для завершения введите этот код:
                                </p>
                                <div style=""background-color: #e6f2f8; border-radius: 8px; padding: 15px; text-align: center; margin: 20px 0; border: 1px dashed #86c5da;"">
                                    <p style=""font-size: 32px; font-weight: bold; letter-spacing: 5px; color: #2c3e50; margin: 0;"">
                                        {0}
                                    </p>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style=""padding: 20px; text-align: center; background-color: #a8d8ea; border-radius: 0 0 10px 10px; color: #2c3e50; font-size: 14px;"">
                                <p style=""margin: 5px 0;"">С уважением,</p>
                                <p style=""margin: 5px 0; font-weight: bold;"">Ваш любимый тестировщик</p>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>
                ", code);
                mail.IsBodyHtml = true;
                mail.Subject = "Регистрация диплома";

                await SendMail(mail, emailSettings.EmailPassword);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
        /// <summary>
        /// Отправка письма
        /// </summary>
        /// <param name="mail">письмо</param>
        /// <param name="password">пароль</param>
        /// <returns>ничего(</returns>
        private static async Task SendMail(MailMessage mail, string password)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.yandex.ru", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(mail.From.Address, password);
                    await smtpClient.SendMailAsync(mail);
                }
            }
            catch { }
        }
        /// <summary>
        /// Присвоение пароля пользователю
        /// </summary>
        /// <param name="password">пароль</param>
        /// <returns>ничего(</returns>
        public async static Task SetPassword(string password)
        {
            try
            {
                HomeMenuContext context = new();
                var user = context.Users.FirstOrDefault(u => u.Email == Data.email);
                if (user != null)
                {
                    user.Password = password;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="email">почта</param>
        /// <param name="password">пароль</param>
        /// <returns>успешность авторизации</returns>
        public static bool Authorize(string email, string password)
        {
            try
            {
                HomeMenuContext context = new();
                return context.Users.Any(u => u.Email == email && u.Password == password);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                return false;
            }
        }
        /// <summary>
        /// Сброс пароля
        /// </summary>
        /// <param name="email">почта</param>
        /// <returns>ничего(</returns>
        public async static Task ForgetPassword(string email)
        {
            HomeMenuContext context = new();
            try
            {
                var emailSettings = Privacy.ConfigurationHelper.GetEmailSettings();
                MailAddress fromAddress = new(emailSettings.Email, "Диплом");
                MailAddress toAddress = new(email);
                MailMessage mail = new(fromAddress, toAddress);

                var rnd = new Random();
                var code = rnd.Next(100000, 999999);

                var user = context.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    user.Code = code;
                    context.Users.Update(user);
                    await context.SaveChangesAsync();

                    Data.email = email;

                    mail.Body = string.Format(@"
                    <!DOCTYPE html>
                    <html lang=""ru"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Код подтверждения</title>
                    </head>
                    <body style=""font-family: 'Arial', sans-serif; margin: 0; padding: 0; background-color: #f5f9ff;"">
                        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" style=""max-width: 600px; margin: 20px auto;"">
                            <tr>
                                <td style=""padding: 25px 20px; text-align: center; background-color: #a8d8ea; border-radius: 10px 10px 0 0;"">
                                    <h1 style=""color: #2c3e50; margin: 0; font-size: 24px;"">Ваш код подтверждения</h1>
                                </td>
                            </tr>
                            <tr>
                                <td style=""padding: 30px 20px; background-color: #ffffff; border-left: 1px solid #e0e0e0; border-right: 1px solid #e0e0e0;"">
                                    <p style=""color: #2c3e50; font-size: 16px; line-height: 1.5; margin-bottom: 25px;"">
                                        Для восстановления пароля введите этот код:
                                    </p>
                                    <div style=""background-color: #e6f2f8; border-radius: 8px; padding: 15px; text-align: center; margin: 20px 0; border: 1px dashed #86c5da;"">
                                        <p style=""font-size: 32px; font-weight: bold; letter-spacing: 5px; color: #2c3e50; margin: 0;"">
                                            {0}
                                        </p>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style=""padding: 20px; text-align: center; background-color: #a8d8ea; border-radius: 0 0 10px 10px; color: #2c3e50; font-size: 14px;"">
                                    <p style=""margin: 5px 0;"">С уважением,</p>
                                    <p style=""margin: 5px 0; font-weight: bold;"">Ваш любимый тестировщик</p>
                                </td>
                            </tr>
                        </table>
                    </body>
                    </html>
                    ", code);
                    mail.IsBodyHtml = true;
                    mail.Subject = "Восстановление пароля диплома";

                    await SendMail(mail, emailSettings.EmailPassword);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }
}
