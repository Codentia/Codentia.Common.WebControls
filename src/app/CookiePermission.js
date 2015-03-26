function CookiePermission_AcceptCookies()
{
    var checked = document.getElementById(_cookiePermissionCheckBoxId).checked;

    if (checked == true || checked == 'yes')
    {
        var today = new Date();
        today.setTime(today.getTime());
        var expires_date = new Date(today.getTime() + 365 * 1000 * 60 * 60 * 24);

        document.cookie = _cookiePermissionName + '=1;domain' + document.domain + ';expires=' + expires_date.toGMTString();
        document.getElementById(_cookiePermissionId).visibility = 'hidden';
        document.getElementById(_cookiePermissionId).style.display = 'none';
    }
    else
    {
        alert('You must check the box to confirm your acceptance of cookies from this site.');
    }

    return false;
}

