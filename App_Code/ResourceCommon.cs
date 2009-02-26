/**
@file
    ResourceCommon.cs
@brief
    Copyright 2009 Company. All rights reserved.
@author
    William Chang
@version
    0.1
@date
    - Created: 2008-08-29
    - Modified: 2009-01-29
    .
@note
    References:
    - General:
        - http://weblogs.asp.net/rajbk/archive/2003/12/11/setting-the-default-button-for-a-textbox-in-asp-net.aspx
        .
    .
*/

using System;
using System.Data;
using System.Collections.Generic;
using System.Web;

namespace ent {

/// <summary>Class ResourceCommon</summary>
public class ResourceCommon {

#region Fields
    
    // URL (URI).
    public const String urlProtocol_Main = "http://";
    public const String urlProtocol_Secure = "https://";
    // Login, Forget.
    public const String msgError_LoginInvalid = "Invalid login. Please try again.";
    public const String msgError_LoginForgetEmail = "Email does not exist. Please try again.";
    // Crud.
    public const String msgError_NoTableSelection = "Error: No selection was made. Please try again by selecting inside the table to highlight the row.";
    public const String msgError_NoListSelection = "Error: No selection was made. Please try again by selecting from the list.";
    public const String msgError_UserImpersonateFailed = "Error: Cannot impersonate user because it contains no further data or the user never has login.";
    // Registration.
    public const String msgLog_RegistrationSuccess = "Your registration has been submitted/updated.";
    public const String msgLog_RegistrationSuccessActivation = "Your registration has been submitted/updated for approval. If your registration has not been approved for sometime please contact the management team.";
    public const String msgError_RegistrationUnknown = "Your registration is already submitted, not activated, or in process for deletion.";
    public const String msgError_RegistrationDoesNotExist = "Your registration does not exist. Please check if your are login.";
    public const String msgLog_RegistrationDisabled = "Access to this form is disabled by management.";
    // Authentication And Forgotten Accounts.
    public const String msgLog_AccountCreationSuccess = "Your user account has been created and the activation link has been sent to your email you entered. Note that you must activate the account by clicking on the activation link when you get the email and create a password before you login.";
    public const String msgLog_AccountActivationSuccess = "Your user account has been activated/recovered. Please login with your username and password below.";
    public const String msgError_AccountDuplicate = "The username or email is already in use. Please choose a different username or email.";
    public const String msgError_AccountInvalid = "Your registration is not updated. The email is already in use or your current password is invalid.";
    public const String msgError_AccountActivationInvalid = "Your activation key is invalid. Please try resetting your user account again.";
    public const String qsError_RegistrationInvalidSession = "You+are+not+login+or+you+must+create+a+user+account.";
    public const String qsError_RegistrationUnconfim = "You+are+registration+is+unconfirm.";
    public const String qsError_UserProfileUnregistered = "You+must+create+a+user+profile.";
    // Session.
    public const String qsError_SessionKeyRequired = "You+must+have+a+session+key+to+access+this+page.";
    public const String qsError_SessionKeyInvalid = "Your+session+key+is+invalid+or+expired.+Please+contact+the+management+team+to+be+reactivated.";
    // Form.
    public const String frmError_CheckboxNoSelection = "Error: No selection was made from the checkbox.";
    public const String frmError_NoQueryString = "Error: The form you requested is unavailable or does not exist.";
    // Email.
    public const String emlLog_Success = "Email sent successfully.";
    public const String emlError_Failure = "Error sending email.";
    public const String emlBodyFooter_Generic = @"
        <p>This is a post-only mailing.  Replies to this message are not monitored
        or answered.</p>
        ";
    // Account Activation.
    public const String emlSubject_AccountActivation = "Account Activation - Engineering Technology";
    public const String emlBodyHeader_AccountActivation = @"
        <p>Welcome to Engineering Technology.  Your account is currently inactive.
        To activate your account and verify your e-mail address, please click
        on the following link:</p>
        ";
    public const String emlBodyFooter_AccountActivation = @"
        <p>If you have received this mail in error, you do not need to take any
        action to cancel the account. The account will not be activated, and
        you will not receive any further emails.</p>

        <p>If clicking the link above does not work, copy and paste the URL in a
        new browser window instead.</p>

        <p>Thank you for using Engineering Technology.</p>

        <p>This is a post-only mailing.  Replies to this message are not monitored
        or answered.</p>
        ";
    public const String emlUrl_AccountActivation = "/main/register_user_account.aspx";
    // Account Reset.
    public const String emlSubject_AccountReset = "Account Reset - Engineering Technology";
    public const String emlBodyHeader_AccountReset = @"
        <p>You requested that your username and password for Engineering Technology be
        sent to you via email. For security reasons, your password has been
        encrypted in our database and we cannot retrieve it for you.</p>

        <p>To change your password, follow the link below which will take you
        to the change password page where you can enter a new password.</p>

        <p>If you did not request this email, you can safely ignore it.</p>
        ";
    public const String emlBodyFooter_AccountReset = @"
        <p>Thank you for using Engineering Technology.</p>

        <p>This is a post-only mailing.  Replies to this message are not monitored
        or answered.</p>
        ";
    public const String emlUrl_AccountReset = "/main/register_user_account.aspx";

#endregion

    /// <summary>Default constructor.</summary>
    public ResourceCommon() {}

#region Populate

    /// <summary>Get list of yes or no.</summary>
    public static System.Collections.SortedList getListYesNo() {
        System.Collections.SortedList list = new System.Collections.SortedList();

        list.Add("", "");
        list.Add("Yes", true);
        list.Add("No", false);
        return list;
    }
    /// <summary>Get list of type of schools.</summary>
    public static System.Collections.ArrayList getListSalutations() {
        System.Collections.ArrayList list = new System.Collections.ArrayList();

        list.Add("");
        list.Add("Mr");
        list.Add("Ms");
        list.Add("Mrs");
        list.Add("Dr");
        return list;
    }
    /// <summary>Get list of USA states.</summary>
    /// <remarks>http://lab.artlung.com/50states/</remarks>
    public static System.Collections.SortedList getListStates() {
        System.Collections.SortedList list = new System.Collections.SortedList();

        list.Add("", "");
        list.Add("Alaska", "AK");
        list.Add("Alabama", "AL");
        list.Add("Arkansas", "AR");
        list.Add("Arizona", "AZ");
        list.Add("California", "CA");
        list.Add("Colorado", "CO");
        list.Add("Connecticut", "CT");
        list.Add("District of Columbia", "DC");
        list.Add("Delaware", "DE");
        list.Add("Florida", "FL");
        list.Add("Georgia", "GA");
        list.Add("Hawaii", "HI");
        list.Add("Iowa", "IA");
        list.Add("Idaho", "ID");
        list.Add("Illinois", "IL");
        list.Add("Indiana", "IN");
        list.Add("Kansas", "KS");
        list.Add("Kentucky", "KY");
        list.Add("Louisiana", "LA");
        list.Add("Massachusetts", "MA");
        list.Add("Maryland", "MD");
        list.Add("Maine", "ME");
        list.Add("Michigan", "MI");
        list.Add("Minnesota", "MN");
        list.Add("Missouri", "MO");
        list.Add("Mississippi", "MS");
        list.Add("Montana", "MT");
        list.Add("North Carolina", "NC");
        list.Add("North Dakota", "ND");
        list.Add("Nebraska", "NE");
        list.Add("New Hampshire", "NH");
        list.Add("New Jersey", "NJ");
        list.Add("New Mexico", "NM");
        list.Add("Nevada", "NV");
        list.Add("New York", "NY");
        list.Add("Ohio", "OH");
        list.Add("Oklahoma", "OK");
        list.Add("Oregon", "OR");
        list.Add("Pennsylvania", "PA");
        list.Add("Rhode Island", "RI");
        list.Add("South Carolina", "SC");
        list.Add("South Dakota", "SD");
        list.Add("Tennessee", "TN");
        list.Add("Texas", "TX");
        list.Add("Utah", "UT");
        list.Add("Virginia", "VA");
        list.Add("Vermont", "VT");
        list.Add("Washington", "WA");
        list.Add("Wisconsin", "WI");
        list.Add("West Virginia", "WV");
        list.Add("Wyoming", "WY");
        return list;
    }
    /// <summary>Get list of countries.</summary>
    /// <remarks>http://javascript.about.com/library/blcountry.htm</remarks>
    public static System.Collections.ArrayList getListCountries() {
        System.Collections.ArrayList list = new System.Collections.ArrayList();

        list.Add("");
        list.Add("Afghanistan");
        list.Add("Åland Islands");
        list.Add("Albania");
        list.Add("Algeria");
        list.Add("American Samoa");
        list.Add("Andorra");
        list.Add("Angola");
        list.Add("Anguilla");
        list.Add("Antarctica");
        list.Add("Antigua and Barbuda");
        list.Add("Argentina");
        list.Add("Armenia");
        list.Add("Aruba");
        list.Add("Australia");
        list.Add("Austria");
        list.Add("Azerbaijan");
        list.Add("Bahamas");
        list.Add("Bahrain");
        list.Add("Bangladesh");
        list.Add("Barbados");
        list.Add("Belarus");
        list.Add("Belgium");
        list.Add("Belize");
        list.Add("Benin");
        list.Add("Bermuda");
        list.Add("Bhutan");
        list.Add("Bolivia");
        list.Add("Bosnia and Herzegovina");
        list.Add("Botswana");
        list.Add("Bouvet Island");
        list.Add("Brazil");
        list.Add("British Indian Ocean territory");
        list.Add("Brunei Darussalam");
        list.Add("Bulgaria");
        list.Add("Burkina Faso");
        list.Add("Burundi");
        list.Add("Cambodia");
        list.Add("Cameroon");
        list.Add("Canada");
        list.Add("Cape Verde");
        list.Add("Cayman Islands");
        list.Add("Central African Republic");
        list.Add("Chad");
        list.Add("Chile");
        list.Add("China");
        list.Add("Christmas Island");
        list.Add("Cocos (Keeling) Islands");
        list.Add("Colombia");
        list.Add("Comoros");
        list.Add("Congo");
        list.Add("Congo, Democratic Republic");
        list.Add("Cook Islands");
        list.Add("Costa Rica");
        list.Add("Côte d'Ivoire (Ivory Coast)");
        list.Add("Croatia (Hrvatska)");
        list.Add("Cuba");
        list.Add("Cyprus");
        list.Add("Czech Republic");
        list.Add("Denmark");
        list.Add("Djibouti");
        list.Add("Dominica");
        list.Add("Dominican Republic");
        list.Add("East Timor");
        list.Add("Ecuador");
        list.Add("Egypt");
        list.Add("El Salvador");
        list.Add("Equatorial Guinea");
        list.Add("Eritrea");
        list.Add("Estonia");
        list.Add("Ethiopia");
        list.Add("Falkland Islands");
        list.Add("Faroe Islands");
        list.Add("Fiji");
        list.Add("Finland");
        list.Add("France");
        list.Add("French Guiana");
        list.Add("French Polynesia");
        list.Add("French Southern Territories");
        list.Add("Gabon");
        list.Add("Gambia");
        list.Add("Georgia");
        list.Add("Germany");
        list.Add("Ghana");
        list.Add("Gibraltar");
        list.Add("Greece");
        list.Add("Greenland");
        list.Add("Grenada");
        list.Add("Guadeloupe");
        list.Add("Guam");
        list.Add("Guatemala");
        list.Add("Guinea");
        list.Add("Guinea-Bissau");
        list.Add("Guyana");
        list.Add("Haiti");
        list.Add("Heard and McDonald Islands");
        list.Add("Honduras");
        list.Add("Hong Kong");
        list.Add("Hungary");
        list.Add("Iceland");
        list.Add("India");
        list.Add("Indonesia");
        list.Add("Iran");
        list.Add("Iraq");
        list.Add("Ireland");
        list.Add("Israel");
        list.Add("Italy");
        list.Add("Jamaica");
        list.Add("Japan");
        list.Add("Jordan");
        list.Add("Kazakhstan");
        list.Add("Kenya");
        list.Add("Kiribati");
        list.Add("Korea (north)");
        list.Add("Korea (south)");
        list.Add("Kuwait");
        list.Add("Kyrgyzstan");
        list.Add("Lao People's Democratic Republic");
        list.Add("Latvia");
        list.Add("Lebanon");
        list.Add("Lesotho");
        list.Add("Liberia");
        list.Add("Libyan Arab Jamahiriya");
        list.Add("Liechtenstein");
        list.Add("Lithuania");
        list.Add("Luxembourg");
        list.Add("Macao");
        list.Add("Macedonia (FYROM)");
        list.Add("Madagascar");
        list.Add("Malawi");
        list.Add("Malaysia");
        list.Add("Maldives");
        list.Add("Mali");
        list.Add("Malta");
        list.Add("Marshall Islands");
        list.Add("Martinique");
        list.Add("Mauritania");
        list.Add("Mauritius");
        list.Add("Mayotte");
        list.Add("Mexico");
        list.Add("Micronesia");
        list.Add("Moldova");
        list.Add("Monaco");
        list.Add("Mongolia");
        list.Add("Montserrat");
        list.Add("Morocco");
        list.Add("Mozambique");
        list.Add("Myanmar");
        list.Add("Namibia");
        list.Add("Nauru");
        list.Add("Nepal");
        list.Add("Netherlands");
        list.Add("Netherlands Antilles");
        list.Add("New Caledonia");
        list.Add("New Zealand");
        list.Add("Nicaragua");
        list.Add("Niger");
        list.Add("Nigeria");
        list.Add("Niue");
        list.Add("Norfolk Island");
        list.Add("Northern Mariana Islands");
        list.Add("Norway");
        list.Add("Oman");
        list.Add("Pakistan");
        list.Add("Palau");
        list.Add("Palestinian Territories");
        list.Add("Panama");
        list.Add("Papua New Guinea");
        list.Add("Paraguay");
        list.Add("Peru");
        list.Add("Philippines");
        list.Add("Pitcairn");
        list.Add("Poland");
        list.Add("Portugal");
        list.Add("Puerto Rico");
        list.Add("Qatar");
        list.Add("Réunion");
        list.Add("Romania");
        list.Add("Russian Federation");
        list.Add("Rwanda");
        list.Add("Saint Helena");
        list.Add("Saint Kitts and Nevis");
        list.Add("Saint Lucia");
        list.Add("Saint Pierre and Miquelon");
        list.Add("Saint Vincent and the Grenadines");
        list.Add("Samoa");
        list.Add("San Marino");
        list.Add("Sao Tome and Principe");
        list.Add("Saudi Arabia");
        list.Add("Senegal");
        list.Add("Serbia and Montenegro");
        list.Add("Seychelles");
        list.Add("Sierra Leone");
        list.Add("Singapore");
        list.Add("Slovakia");
        list.Add("Slovenia");
        list.Add("Solomon Islands");
        list.Add("Somalia");
        list.Add("South Africa");
        list.Add("South Georgia and the South Sandwich Islands");
        list.Add("Spain");
        list.Add("Sri Lanka");
        list.Add("Sudan");
        list.Add("Suriname");
        list.Add("Svalbard and Jan Mayen Islands");
        list.Add("Swaziland");
        list.Add("Sweden");
        list.Add("Switzerland");
        list.Add("Syria");
        list.Add("Taiwan");
        list.Add("Tajikistan");
        list.Add("Tanzania");
        list.Add("Thailand");
        list.Add("Togo");
        list.Add("Tokelau");
        list.Add("Tonga");
        list.Add("Trinidad and Tobago");
        list.Add("Tunisia");
        list.Add("Turkey");
        list.Add("Turkmenistan");
        list.Add("Turks and Caicos Islands");
        list.Add("Tuvalu");
        list.Add("Uganda");
        list.Add("Ukraine");
        list.Add("United Arab Emirates");
        list.Add("United Kingdom");
        list.Add("United States of America");
        list.Add("Uruguay");
        list.Add("Uzbekistan");
        list.Add("Vanuatu");
        list.Add("Vatican City");
        list.Add("Venezuela");
        list.Add("Vietnam");
        list.Add("Virgin Islands (British)");
        list.Add("Virgin Islands (US)");
        list.Add("Wallis and Futuna Islands");
        list.Add("Western Sahara");
        list.Add("Yemen");
        list.Add("Zaire");
        list.Add("Zambia");
        list.Add("Zimbabwe");
        return list;
    }

#endregion

}

} // END namespace ent
