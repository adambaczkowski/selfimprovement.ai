if ($args.Length -eq 0) {
    Write-Host "Usage: ./send_email.ps1 <message>"
    Exit
}
##########################################################################
$smtpServer = "localhost"
$smtpPort = 1025
$to = "noreply@seflimprovement.ai"
$subject = "test message"
##########################################################################
$message = $args[0]
$smtp = New-Object Net.Mail.SmtpClient($smtpServer, $smtpPort)
$mailMessage = New-Object Net.Mail.MailMessage($to, $to, $subject, $message)

try {
    $smtp.Send($mailMessage)
    Write-Host "Email sent successfully!"
} catch {
    Write-Host "Error sending email: $_"
}
