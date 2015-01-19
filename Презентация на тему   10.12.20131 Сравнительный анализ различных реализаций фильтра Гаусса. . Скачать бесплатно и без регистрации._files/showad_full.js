(function () {

function btw_ad_close(id) {
    elm_ad = document.getElementById(id);
    if (elm_ad) {
        elm_ad.style.display = 'none';
    }
}

function btw_data_check(data, section)
{
    return (data['provider']
        && data['provider'] == 'between'
        && data['section'] == section
        && ! data['is_show']
    )
}

function btw_getWindowSize()
{
    var winWidth,winHeight;
    if( typeof( window.innerWidth ) == 'number' ) {
        //Non-IE
        winWidth = window.innerWidth;
        winHeight = window.innerHeight;
    } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
        //IE 6+ in 'standards compliant mode'
        winWidth = document.documentElement.clientWidth;
        winHeight = document.documentElement.clientHeight;
    } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
        //IE 4 compatible
        winWidth = document.body.clientWidth;
        winHeight = document.body.clientHeight;
    }
    return {"width":winWidth, "height":winHeight};
}//END function btw_getWindowSize


function btw_getBodyScrollTop()
{
    return self.pageYOffset || (document.documentElement && document.documentElement.scrollTop) || (document.body && document.body.scrollTop);
} //END function btw_getBodyScrollTop

function btw_getBodyScrollLeft()
{
    return self.pageXOffset || (document.documentElement && document.documentElement.scrollLeft) || (document.body && document.body.scrollLeft);
} //END function btw_getBodyScrollLeft

function btw_absPosition(obj)
{
    var x = y = 0;
    while(obj) {
        x += obj.offsetLeft;
        y += obj.offsetTop;
        obj = obj.offsetParent;
    }
    return {x:x, y:y};
} //END function btw_absPosition

function is_btw_show()
{
    var wind_size = btw_getWindowSize();
    var posY = wind_size.height + btw_getBodyScrollTop();
    var posX = wind_size.width + btw_getBodyScrollLeft();
    var position = btw_absPosition(document.getElementById("btw_ad_" + btw_section));
    return (posY >= position.y && posX >= position.x)
}

function btw_show()
{
    if ( ! is_show_btw ) {
        document.getElementById("btw_ad_" + btw_section).src = baseURL;
        is_show_btw = true;
    }
}

function is_mode_visibility_only() {
    return (typeof btw_show_in_visible != 'undefined' && btw_show_in_visible == true)
}

var baseURL = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'ads.betweendigital.com/'; // This URL should be changed.

// Get TZ offset
var tzOffset = new Date().getTimezoneOffset();
var is_show_btw = false;

// Get major flash version
var flashVersion = 0;

if (typeof navigator.plugins != 'undefined' && typeof navigator.plugins["Shockwave Flash"] == "object" ) {
    var d = navigator.plugins["Shockwave Flash"].description;
    if (d && !(typeof navigator.mimeTypes != 'undefined' && navigator.mimeTypes["application/x-shockwave-flash"] && !navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin)) {
        d = d.replace(/^.*\s+(\S+\s+\S+$)/, "$1");
        flashVersion = parseInt(d.replace(/^(.*)\..*$/, "$1"), 10);
    }
} else if (typeof window.ActiveXObject != 'undefined') {
    try {
        var a = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
        if (a) {
            var d = a.GetVariable("$version");
            if (d) {
                d = d.split(" ")[1].split(",");
                flashVersion = parseInt(d[0], 10);
            }
        }
    }
    catch(e) {}
}

// Get referer
var refererURL = '';
if (top!=self)refererURL = encodeURIComponent(document.referrer);

/* Calculate visibility */
// Add new transparent pixel, so we can get it's offset
pix_id = typeof pix_id != 'undefined' ? pix_id : "tpix_2392818"
document.write(
  "<img id=\""+ pix_id+"\" src=\"http:\/\/ads.betweendigital.com\/1x1.gif\" alt=\" \" style=\"display:none\"\/>"
);

// Trying to calculate pixel's offsetY
var pix = document.getElementById ? document.getElementById(pix_id) : document.all[pix_id];
if (pix) {
	pix.style.visibility = 'hidden'; 
	pix.style.position = 'absolute';
	pix.style.display = 'block';
}
var pos = pix ? pix.offsetTop : 0;
while (pix && (pix.offsetParent != null)) {
   pix = pix.offsetParent;
   pos += pix.offsetTop;
   if (pix.tagName == 'BODY') break;
}

// Trying to calculate browser window height
var winHeight = 0;

if( typeof( window.innerHeight ) == 'number' ) {
    // Non-IE
    winHeight = window.innerHeight;
} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
    // IE 6+ in 'standards compliant mode'
    winHeight = document.documentElement.clientHeight;
} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
    // IE 4 compatible
    winHeight = document.body.clientHeight;
}

// Compare and save
if ((window == top) && (pos || (pos === 0)) && winHeight ) { // If winHeight === 0, there is something wrong, so report as N/A
    pos > winHeight ? pos='btf' : pos='atf';
} else {
    pos='';
}

if (is_mode_visibility_only()) {
    // check position in iframe
    if (window == top) {
        pos='atf';
    } else {
        pos='';
    }
 }

// get iframe level
var btw_frm_level = 0;
var _btw_cnt = window;
while (window.top != _btw_cnt.window) {
   btw_frm_level++;
   _btw_cnt = _btw_cnt.parent;
   // if level is large that break from cycle
   if (btw_frm_level >= 20) {
      break;
   }
}
// end get iframe level


// Store all data
var params = [ 'ref='+refererURL, 'tz='+tzOffset, 'fl='+flashVersion, 'pos='+pos, 'frl='+btw_frm_level, 'ord='+Math.random()*10000000000000000 ];

var embedType = 'adj'; // serve with 'script' tag by default
var size=[];
var btw_section = 0
// Add user-defined params to final URL
if (_up instanceof Array ) {
    for (var i=0; i< _up.length; i++) {
        if (!_up[i] instanceof Array) continue;
        if (_up[i] == null) continue;
        switch (_up[i][0]) {
            case 'tagType':
                embedType = _up[i][1];
                break;
            case 's':
                btw_section = _up[i][1];
                params.push(_up[i][0]+'='+_up[i][1]);
                break;
            case 'w':
                size[0] = _up[i][1];
            case 'h':
                size[1] = _up[i][1];
            default:
                params.push(_up[i][0]+'='+_up[i][1]);
                break;
        }
    }
}

if (is_mode_visibility_only()) {
    embedType = 'adi';
}

baseURL += embedType + '?' + params.join('&');

function listener(event) {
    if( event.origin != 'http://ads.betweendigital.com') {
        return;
    }
    if (btw_data_check(event.data, btw_section)) {
        btw_ad_close('btw_ad_'+btw_section);
    }

}

if (window.addEventListener){
    window.addEventListener("message", listener, false);
} else {
    window.attachEvent("onmessage", listener);
}

if (is_mode_visibility_only())
{
    var fn_onscroll = window.onscroll;
    window.onscroll = function() {
        if (fn_onscroll) {
            fn_onscroll();
        }
        if (is_btw_show()) {
            btw_show();
        }
    }
    document.write("<iframe id='btw_ad_" + btw_section + "' style='border:none;height:"+size[1]+"px;width:"+size[0]+"px' width='" + size[0] +"' height='" + size[1]+ "' border='0' scrolling='no'></iframe>");
    if (is_btw_show()) {
        btw_show();
    }
} else {
    if (embedType == 'adj') {
        document.write("<script type='text/javascript' src='"+ baseURL +"'></script>");
    } else {
        document.write("<iframe id='btw_ad_" + btw_section + "' src='"+ baseURL +"' style='border:none;height:"+size[1]+"px;width:"+size[0]+"px' width='" + size[0] +"' height='" + size[1]+ "' border='0' scrolling='no'></iframe>");
    }
}

})();