function show_close_btn(cnt, ad_type)
{
    var btw_adv = document.getElementById('btw_adv');
    var is_btw_show = false;
    if (btw_adv) {
        var btw_da_img = btw_adv.getElementsByTagName('img')
        var btw_da_frm = btw_adv.getElementsByTagName('iframe')
         if (btw_da_img[0] || btw_da_frm[0]) {
            is_btw_show = true;
            if (ad_type == 'fullscreen') {
                btw_tm = new Date().getTime();
                document.cookie='btw_show_madv='+btw_tm+';path=/';
            }
        }
    }
    if (!is_btw_show && cnt<10) {
        setTimeout(function() { show_close_btn(cnt+1, ad_type) }, 1000);
    }
}


function get_close_link_size()
{
    return Math.min(128, Math.round(Math.min(getDocumentWidth() * 0.1,  getDocumentHeight() * 0.1)));
}

function getDocumentHeight(){
    return (document.body.scrollHeight > document.body.offsetHeight) ? document.body.scrollHeight: document.body.offsetHeight;
}

function getDocumentWidth(){
    return (document.body.scrollWidth > document.body.offsetWidth) ? document.body.scrollWidth: document.body.offsetWidth;
}

function adv_rem() {
    document.getElementById('btw_adv').style.display='none';
    document.getElementById('btw_layout').style.display = 'none';
    document.getElementById('link_close').style.display = 'none';

}

function getCookie(name) {
  var value = "; " + document.cookie;
  var parts = value.split("; " + name + "=");
  if (parts.length == 2) {
        return parts.pop().split(";").shift();
  }
  return undefined;
}


(function () {

if (document.getElementById('btw_adv')) {
    return;
}
if (window.m_up == undefined || m_up instanceof Array != true) {
    return;
}
if (window != window.top) {
    return;
}
var ismobile = navigator.userAgent.match(
    /iphone|ipod|android|blackberry|opera mini|opera mobi|skyfire|maemo|windows phone|palm|iemobile|symbian|symbianos|fennec/i
)
var istablet = navigator.userAgent.match(
    /ipad|android 3|Silk-Accelerated|sch-i800|playbook|tablet|kindle|gt-p1000|GT-P|sgh-t849|shw-m180s|a510|a511|a100|dell streak|silk/i
)

if (ismobile == null && istablet == null) {
    return;
}
btw_last_time_madv = getCookie('btw_show_madv');
t_now = new Date().getTime();
if (((t_now - btw_last_time_madv)/1000/60) < 15) {
    return;
}





var baseURL = ('https:' == document.location.protocol ? 'https://' : 'http://')
            + 'ads.betweendigital.com/'; // This URL should be changed.
// Get TZ offset
var tzOffset = new Date().getTimezoneOffset();
// Get major flash version
var flashVersion = 0;
if (typeof navigator.plugins != undefined
    && typeof navigator.plugins["Shockwave Flash"] == "object" ) {
    var d = navigator.plugins["Shockwave Flash"].description;
    if (d && !(typeof navigator.mimeTypes != undefined
        && navigator.mimeTypes["application/x-shockwave-flash"]
        && !navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin)) {
        d = d.replace(/^.*\s+(\S+\s+\S+$)/, "$1");
        flashVersion = parseInt(d.replace(/^(.*)\..*$/, "$1"), 10);
    }
} else if (typeof window.ActiveXObject != undefined) {
    try {
        var a = new ActiveXObject("ShockwaveFlash.ShockwaveFlash");
        if (a) {
            var d = a.GetVariable("$version");
            if (d) {
                d = d.split(" ")[1].split(",");
                flashVerstion = parseInt(d[0], 10);
            }
        }
    }
    catch(e) {}
}
// Get referer
var refererURL = '';
if (top!=self)refererURL = encodeURIComponent(document.referrer);
section = 0;
ad_type = 'floating'
// Add user-defined params to final URL
if (m_up instanceof Array ) {
    for (var i=0; i< m_up.length; i++) {
        if (!m_up[i] instanceof Array) continue;
        switch (m_up[i][0]) {
            case 's':
                section = m_up[i][1];
                break;
            case 's_type':
                ad_type = m_up[i][1];
                break;
        }
    }
}


setTimeout(function() { show_close_btn(1, ad_type) }, 1000);
block_size = false;

if (ad_type == 'fullscreen') {
    btw_w = window.screen.width;
    btw_h = window.screen.height;
    block_size = {'w': btw_w, 'h': btw_h}
} else {
    // определяем размеры устройства и подходящего блока
    screen_size = [
        {'w':728, 'h':90},
        {'w':600, 'h':100},
        {'w':450, 'h':75},
        {'w':320, 'h':50},
        {'w':300, 'h':50},
        {'w':216, 'h':36},
        {'w':168, 'h':28},
        {'w':120, 'h':20},
    ];

    for (var i = 0; i<screen_size.length; i++)
    {
        if (document.body.clientWidth > screen_size[i]['w'] &&
            document.body.clientHeight > screen_size[i]['h'])
        {
            block_size = screen_size[i];
            break;
        }
    }

}
if (block_size == false) {
       // нет подходящих размеров
       return;
    }

// Store all data
var params = [
    'ref='+refererURL,
    'tz='+tzOffset,
    'fl='+flashVersion,
    'w='+block_size['w'],
    'h='+block_size['h'],
    's='+section,
    'pos=atf'
];
var embedType = 'adj';
baseURL += embedType + '?' + params.join('&');


if (ad_type == 'fullscreen') {
    document.write("<div id='btw_adv'><script src='"+ baseURL +"'></scri"+"pt></div>");
} else {
    document.write(
      "<div id='btw_adv' style='z-index: 9999999;position:fixed; bottom:0px;left:0px;right:0px;width:100%;margin-left:auto;margin-right:auto;text-align:center;'>"
       + "<div style='position:absolute;top:-25px;left:0;right:0;text-align: center;'>"
       + "<a id='link_close' style='z-index: 9999999;height:25px;display:none;margin-left:auto;margin-right:auto;width:25px;background-image:url(http://cache.betweendigital.com/code/close_down.png);background-repeat: no-repeat;padding:0px 8px;width:50px;' href='javascript:void(0)' onclick='adv_rem()'></a>"
       + "</div><script type='text/javascript' src='"+ baseURL +"'></scr"+"ipt>"
       + "</div>");
}

})();