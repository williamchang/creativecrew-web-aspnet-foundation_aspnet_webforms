/**
@file
    ApplicationEmail.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.1
@date
    - Created: 2008-08-29
    - Modified: 2009-02-26
    .
@note
    References:
    - General:
        - http://en.wikipedia.org/wiki/C_Sharp_(programming_language)
        .
    .
*/

using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

namespace ent {

/// <summary>Class ApplicationEmail extends TableCommon</summary>
public class ApplicationEmail : ApplicationCommon {
    /// <summary>Default constructor.</summary>
    public ApplicationEmail() { }
    /// <summary>Send a single email.</summary>
    public Boolean sendEmailSingle(String body, String subject, String toAddress, String fromAddress) {
        try {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(new System.Net.Mail.MailAddress(toAddress));
            msg.From = new System.Net.Mail.MailAddress(fromAddress);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(appSmtp);
            smtp.Send(msg);
            return true;
        } catch {
            return false;
        }
    }
    /// <summary>Send a single email with file attachment.</summary>
    public Boolean sendEmailSingleAttachment(String attachmentFilePath, String body, String subject, String toAddress, String fromAddress) {
        try {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(new System.Net.Mail.MailAddress(toAddress));
            msg.From = new System.Net.Mail.MailAddress(fromAddress);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            msg.Attachments.Add(new System.Net.Mail.Attachment(attachmentFilePath));

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(appSmtp);
            smtp.Send(msg);
            return true;
        } catch {
            return false;
        }
    }
    /// <summary>Send notification email.</summary>
    public Boolean sendEmailNotification(String messageBody, String messageSubject) {
        try {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(new System.Net.Mail.MailAddress(appEmailNotification1));
            msg.To.Add(new System.Net.Mail.MailAddress(appEmailNotification2));
            msg.From = new System.Net.Mail.MailAddress(appEmailManagement, appEmailManagementName);
            msg.Subject = messageSubject;
            msg.Body = messageBody + ResourceCommon.emlBodyFooter_Generic;
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(appSmtp);
            smtp.Send(msg);
            return true;
        } catch {
            return false;
        }
    }
    /// <summary>Send account activation email.</summary>
    public Boolean sendEmailAccountActivation(String username, String activationKey, String toAddress) {
        try {
            String urlActivation = ResourceCommon.urlProtocol_Main + appUrl + ResourceCommon.emlUrl_AccountActivation + "?mode=activation&actkey=" + activationKey;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(new System.Net.Mail.MailAddress(toAddress));
            msg.From = new System.Net.Mail.MailAddress(appEmailMaintenance, appEmailMaintenanceName);
            msg.Subject = ResourceCommon.emlSubject_AccountActivation;
            msg.Body = ResourceCommon.emlBodyHeader_AccountActivation + @"
                <br/><hr/>
                " + username + @"
                <br/><br/>
                <a href=""" + urlActivation + @""">" + urlActivation + @"</a>
                <hr/><br/>
                " + ResourceCommon.emlBodyFooter_AccountActivation;
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.High;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(appSmtp);
            //HACK smtp.Send(msg);
            return true;
        } catch {
            return false;
        }
    }
    /// <summary>Send account reset email.</summary>
    public Boolean sendEmailAccountReset(String username, String activationKey, String toAddress) {
        try {
            String urlActivation = ResourceCommon.urlProtocol_Main + appUrl + ResourceCommon.emlUrl_AccountReset + "?mode=activation&actkey=" + activationKey;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(new System.Net.Mail.MailAddress(toAddress));
            msg.From = new System.Net.Mail.MailAddress(appEmailMaintenance, appEmailMaintenanceName);
            msg.Subject = ResourceCommon.emlSubject_AccountReset;
            msg.Body = ResourceCommon.emlBodyHeader_AccountReset + @"
                <br/><hr/>
                " + username + @"
                <br/><br/>
                <a href=""" + urlActivation + @""">" + urlActivation + @"</a>
                <hr/><br/>
                " + ResourceCommon.emlBodyFooter_AccountReset;
            msg.IsBodyHtml = true;
            msg.Priority = System.Net.Mail.MailPriority.High;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(appSmtp);
            smtp.Send(msg);
            return true;
        } catch {
            return false;
        }
    }
    /// <summary>Send mass email.</summary>
    public Boolean sendEmailMass(String body, String subject, DataTable dtToAddresses, String dtColumnToAddresses, String fromAddress) {
        try {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            foreach(DataRow row in dtToAddresses.Rows) {
                msg.Bcc.Add(new System.Net.Mail.MailAddress(row[dtColumnToAddresses].ToString().Trim()));
            }
            msg.From = new System.Net.Mail.MailAddress(fromAddress);
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(appSmtp);
            //smtp.Send(msg);
            return true;
        } catch {
            return false;
        }
    }
}

} // END namespace ent