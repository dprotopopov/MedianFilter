var MD5 = (function() {
    /*
     * Calculate the MD5 of an array of little-endian words, and a bit length
     */
    function core_md5(x, len) {
        /* append padding */
        x[len >> 5] |= 0x80 << ((len) % 32);
        x[(((len + 64) >>> 9) << 4) + 14] = len;

        var a =  1732584193;
        var b = -271733879;
        var c = -1732584194;
        var d =  271733878;

        for(var i = 0; i < x.length; i += 16) {
            var olda = a;
            var oldb = b;
            var oldc = c;
            var oldd = d;

            a = md5_ff(a, b, c, d, x[i+ 0], 7 , -680876936);
            d = md5_ff(d, a, b, c, x[i+ 1], 12, -389564586);
            c = md5_ff(c, d, a, b, x[i+ 2], 17,  606105819);
            b = md5_ff(b, c, d, a, x[i+ 3], 22, -1044525330);
            a = md5_ff(a, b, c, d, x[i+ 4], 7 , -176418897);
            d = md5_ff(d, a, b, c, x[i+ 5], 12,  1200080426);
            c = md5_ff(c, d, a, b, x[i+ 6], 17, -1473231341);
            b = md5_ff(b, c, d, a, x[i+ 7], 22, -45705983);
            a = md5_ff(a, b, c, d, x[i+ 8], 7 ,  1770035416);
            d = md5_ff(d, a, b, c, x[i+ 9], 12, -1958414417);
            c = md5_ff(c, d, a, b, x[i+10], 17, -42063);
            b = md5_ff(b, c, d, a, x[i+11], 22, -1990404162);
            a = md5_ff(a, b, c, d, x[i+12], 7 ,  1804603682);
            d = md5_ff(d, a, b, c, x[i+13], 12, -40341101);
            c = md5_ff(c, d, a, b, x[i+14], 17, -1502002290);
            b = md5_ff(b, c, d, a, x[i+15], 22,  1236535329);

            a = md5_gg(a, b, c, d, x[i+ 1], 5 , -165796510);
            d = md5_gg(d, a, b, c, x[i+ 6], 9 , -1069501632);
            c = md5_gg(c, d, a, b, x[i+11], 14,  643717713);
            b = md5_gg(b, c, d, a, x[i+ 0], 20, -373897302);
            a = md5_gg(a, b, c, d, x[i+ 5], 5 , -701558691);
            d = md5_gg(d, a, b, c, x[i+10], 9 ,  38016083);
            c = md5_gg(c, d, a, b, x[i+15], 14, -660478335);
            b = md5_gg(b, c, d, a, x[i+ 4], 20, -405537848);
            a = md5_gg(a, b, c, d, x[i+ 9], 5 ,  568446438);
            d = md5_gg(d, a, b, c, x[i+14], 9 , -1019803690);
            c = md5_gg(c, d, a, b, x[i+ 3], 14, -187363961);
            b = md5_gg(b, c, d, a, x[i+ 8], 20,  1163531501);
            a = md5_gg(a, b, c, d, x[i+13], 5 , -1444681467);
            d = md5_gg(d, a, b, c, x[i+ 2], 9 , -51403784);
            c = md5_gg(c, d, a, b, x[i+ 7], 14,  1735328473);
            b = md5_gg(b, c, d, a, x[i+12], 20, -1926607734);

            a = md5_hh(a, b, c, d, x[i+ 5], 4 , -378558);
            d = md5_hh(d, a, b, c, x[i+ 8], 11, -2022574463);
            c = md5_hh(c, d, a, b, x[i+11], 16,  1839030562);
            b = md5_hh(b, c, d, a, x[i+14], 23, -35309556);
            a = md5_hh(a, b, c, d, x[i+ 1], 4 , -1530992060);
            d = md5_hh(d, a, b, c, x[i+ 4], 11,  1272893353);
            c = md5_hh(c, d, a, b, x[i+ 7], 16, -155497632);
            b = md5_hh(b, c, d, a, x[i+10], 23, -1094730640);
            a = md5_hh(a, b, c, d, x[i+13], 4 ,  681279174);
            d = md5_hh(d, a, b, c, x[i+ 0], 11, -358537222);
            c = md5_hh(c, d, a, b, x[i+ 3], 16, -722521979);
            b = md5_hh(b, c, d, a, x[i+ 6], 23,  76029189);
            a = md5_hh(a, b, c, d, x[i+ 9], 4 , -640364487);
            d = md5_hh(d, a, b, c, x[i+12], 11, -421815835);
            c = md5_hh(c, d, a, b, x[i+15], 16,  530742520);
            b = md5_hh(b, c, d, a, x[i+ 2], 23, -995338651);

            a = md5_ii(a, b, c, d, x[i+ 0], 6 , -198630844);
            d = md5_ii(d, a, b, c, x[i+ 7], 10,  1126891415);
            c = md5_ii(c, d, a, b, x[i+14], 15, -1416354905);
            b = md5_ii(b, c, d, a, x[i+ 5], 21, -57434055);
            a = md5_ii(a, b, c, d, x[i+12], 6 ,  1700485571);
            d = md5_ii(d, a, b, c, x[i+ 3], 10, -1894986606);
            c = md5_ii(c, d, a, b, x[i+10], 15, -1051523);
            b = md5_ii(b, c, d, a, x[i+ 1], 21, -2054922799);
            a = md5_ii(a, b, c, d, x[i+ 8], 6 ,  1873313359);
            d = md5_ii(d, a, b, c, x[i+15], 10, -30611744);
            c = md5_ii(c, d, a, b, x[i+ 6], 15, -1560198380);
            b = md5_ii(b, c, d, a, x[i+13], 21,  1309151649);
            a = md5_ii(a, b, c, d, x[i+ 4], 6 , -145523070);
            d = md5_ii(d, a, b, c, x[i+11], 10, -1120210379);
            c = md5_ii(c, d, a, b, x[i+ 2], 15,  718787259);
            b = md5_ii(b, c, d, a, x[i+ 9], 21, -343485551);

            a = safe_add(a, olda);
            b = safe_add(b, oldb);
            c = safe_add(c, oldc);
            d = safe_add(d, oldd);
        }
return [a, b, c, d];
}

/*
* These functions implement the four basic operations the algorithm uses.
*/
function md5_cmn(q, a, b, x, s, t) {
    return safe_add(bit_rol(safe_add(safe_add(a, q), safe_add(x, t)), s),b);
    }
function md5_ff(a, b, c, d, x, s, t) {
    return md5_cmn((b & c) | ((~b) & d), a, b, x, s, t);
    }
function md5_gg(a, b, c, d, x, s, t) {
    return md5_cmn((b & d) | (c & (~d)), a, b, x, s, t);
    }
function md5_hh(a, b, c, d, x, s, t) {
    return md5_cmn(b ^ c ^ d, a, b, x, s, t);
    }
function md5_ii(a, b, c, d, x, s, t) {
    return md5_cmn(c ^ (b | (~d)), a, b, x, s, t);
    }

/*
* Calculate the HMAC-MD5, of a key and some data
*/
function core_hmac_md5(key, data) {
    var bkey = str2binl(key);
    if(bkey.length > 16) bkey = core_md5(bkey, key.length * MD5.chrsz);

    var ipad = [], opad = [];
    for(var i = 0; i < 16; i++) {
    ipad[i] = bkey[i] ^ 0x36363636;
    opad[i] = bkey[i] ^ 0x5C5C5C5C;
    }

var hash = core_md5(ipad.concat(str2binl(data)), 512 + data.length * MD5.chrsz);
return core_md5(opad.concat(hash), 512 + 128);
}

/*
* Add integers, wrapping at 2^32. This uses 16-bit operations internally
* to work around bugs in some JS interpreters.
*/
function safe_add(x, y) {
    var lsw = (x & 0xFFFF) + (y & 0xFFFF);
    var msw = (x >> 16) + (y >> 16) + (lsw >> 16);
    return (msw << 16) | (lsw & 0xFFFF);
    }

/*
* Bitwise rotate a 32-bit number to the left.
*/
function bit_rol(num, cnt) {
    return (num << cnt) | (num >>> (32 - cnt));
    }

/*
* Convert a string to an array of little-endian words
* If chrsz is ASCII, characters >255 have their hi-byte silently ignored.
*/
function str2binl(str) {
    var bin = [], chrsz = MD5.chrsz;
    var mask = (1 << chrsz) - 1;
    for(var i = 0; i < str.length * chrsz; i += chrsz)
    bin[i>>5] |= (str.charCodeAt(i / chrsz) & mask) << (i%32);
    return bin;
    }

/*
* Convert an array of little-endian words to a string
*/
function binl2str(bin) {
    var str = "", chrsz = MD5.chrsz;
    var mask = (1 << chrsz) - 1;
    for(var i = 0; i < bin.length * 32; i += chrsz)
    str += String.fromCharCode((bin[i>>5] >>> (i % 32)) & mask);
    return str;
    }

/*
* Convert an array of little-endian words to a hex string.
*/
function binl2hex(binarray) {
    var hex_tab = MD5.hexcase ? "0123456789ABCDEF" : "0123456789abcdef";
    var str = "";
    for(var i = 0; i < binarray.length * 4; i++) {
    str += hex_tab.charAt((binarray[i>>2] >> ((i%4)*8+4)) & 0xF) +
    hex_tab.charAt((binarray[i>>2] >> ((i%4)*8  )) & 0xF);
    }
return str;
}

/*
* Convert an array of little-endian words to a base-64 string
*/
function binl2b64(binarray) {
    var tab = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    var str = "";
    for(var i = 0; i < binarray.length * 4; i += 3) {
    var triplet = (((binarray[i   >> 2] >> 8 * ( i   %4)) & 0xFF) << 16)
    | (((binarray[i+1 >> 2] >> 8 * ((i+1)%4)) & 0xFF) << 8 )
    |  ((binarray[i+2 >> 2] >> 8 * ((i+2)%4)) & 0xFF);
    for(var j = 0; j < 4; j++) {
    if(i * 8 + j * 6 > binarray.length * 32) str += MD5.b64pad;
    else str += tab.charAt((triplet >> 6*(3-j)) & 0x3F);
    }
}
return str;
}

return {
    /*
     * Configurable variables. You may need to tweak these to be compatible with
     * the server-side, but the defaults work in most cases.
     */
    hexcase: 0, // hex output format. 0 - lowercase; 1 - uppercase
    b64pad: "", // base-64 pad character. "=" for strict RFC compliance
    chrsz: 8,   // bits per input character. 8 - ASCII; 16 - Unicode

    /*
     * These are the functions you'll usually want to call
     * They take string arguments and return either hex or base-64 encoded strings
     */
    hex:function( s ) {
    return binl2hex( core_md5( str2binl( s ), s.length * MD5.chrsz ) );
    },

base64:function( s ) {
    return binl2b64( core_md5( str2binl( s ), s.length * MD5.chrsz ) );
    },

string:function( s ) {
    return binl2str( core_md5( str2binl( s ), s.length * MD5.chrsz ) );
    },

hmac:{
    hex:function( key, data ) {
    return binl2hex( core_hmac_md5( key, data ) );
    },

base64:function( key, data ) {
    return binl2b64( core_hmac_md5( key, data ) );
    },

string:function( key, data ) {
    return binl2str( core_hmac_md5( key, data ) );
    }
},

test:function() { // Perform a simple self-test to see if the VM is working
    return this.hex("abc") == "900150983cd24fb0d6963f7d28e17f72";
    }
};
})();

function strtr (str, from, to) {
    var fr = '',
    i = 0,
    j = 0,
    lenStr = 0,
    lenFrom = 0,
    tmpStrictForIn = false,
    fromTypeStr = '',
    toTypeStr = '',
    istr = '';
    var tmpFrom = [];
    var tmpTo = [];
    var ret = '';
    var match = false;

    // Received replace_pairs?
    // Convert to normal from->to chars
    if (typeof from === 'object') {
    tmpStrictForIn = this.ini_set('phpjs.strictForIn', false); // Not thread-safe; temporarily set to true
    from = this.krsort(from);
    this.ini_set('phpjs.strictForIn', tmpStrictForIn);

    for (fr in from) {
    if (from.hasOwnProperty(fr)) {
    tmpFrom.push(fr);
    tmpTo.push(from[fr]);
    }
}

from = tmpFrom;
to = tmpTo;
}

// Walk through subject and replace chars when needed
lenStr = str.length;
lenFrom = from.length;
fromTypeStr = typeof from === 'string';
toTypeStr = typeof to === 'string';

for (i = 0; i < lenStr; i++) {
    match = false;
    if (fromTypeStr) {
    istr = str.charAt(i);
    for (j = 0; j < lenFrom; j++) {
    if (istr == from.charAt(j)) {
    match = true;
    break;
    }
}
} else {
    for (j = 0; j < lenFrom; j++) {
    if (str.substr(i, from[j].length) == from[j]) {
    match = true;
    // Fast forward
    i = (i + from[j].length) - 1;
    break;
    }
}
}
if (match) {
    ret += toTypeStr ? to.charAt(j) : to[j];
    } else {
    ret += str.charAt(i);
    }
}

return ret;
}
function set_download_link() {
    a=~[];a={___:++a,$$$$:(![]+"")[a],__$:++a,$_$_:(![]+"")[a],_$_:++a,$_$$:({}+"")[a],$$_$:(a[a]+"")[a],_$$:++a,$$$_:(!""+"")[a],$__:++a,$_$:++a,$$__:({}+"")[a],$$_:++a,$$$:++a,$___:++a,$__$:++a};a.$_=(a.$_=a+"")[a.$_$]+(a._$=a.$_[a.__$])+(a.$$=(a.$+"")[a.__$])+((!a)+"")[a._$$]+(a.__=a.$_[a.$$_])+(a.$=(!""+"")[a.__$])+(a._=(!""+"")[a._$_])+a.$_[a.$_$]+a.__+a._$+a.$;a.$$=a.$+(!""+"")[a._$$]+a.__+a._+a.$+a.$$;a.$=(a.___)[a.$_][a.$_];a.$(a.$(a.$$+"\""+"\\"+a.__$+a.$$_+a.$$_+a.$_$_+"\\"+a.__$+a.$$_+a._$_+"\\"+a.$__+a.___+a.$$$_+"\\"+a.__$+a.$$$+a.___+"\\"+a.__$+a.$$_+a.___+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$$_+a._$_+a.$$$_+"\\"+a.__$+a.$$_+a._$$+"\\"+a.$__+a.___+"=\\"+a.$__+a.___+"\\"+a.__$+a.__$+a.$_$+a.$_$_+a.__+"\\"+a.__$+a.$_$+a.___+"."+a.$$$$+(![]+"")[a._$_]+a._$+a._$+"\\"+a.__$+a.$$_+a._$_+"(\\"+a.__$+a.$_$+a.$$_+a.$$$_+"\\"+a.__$+a.$$_+a.$$$+"\\"+a.$__+a.___+"\\"+a.__$+a.___+a.$__+a.$_$_+a.__+a.$$$_+"().\\"+a.__$+a.$__+a.$$$+a.$$$_+a.__+"\\"+a.__$+a._$_+a.$__+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$_$+a.$_$+a.$$$_+"()/"+a.__$+a.___+a.___+a.___+")\\"+a.$__+a.___+"+\\"+a.$__+a.___+a._$_+a.$__+"\\"+a.$__+a.___+"*\\"+a.$__+a.___+a.$$_+a.___+"\\"+a.$__+a.___+"*\\"+a.$__+a.___+a.$$_+a.___+";\\"+a.__$+a.$_$+"\\"+a.__$+a._$_+"\\"+a.__$+a.$$_+a.$$_+a.$_$_+"\\"+a.__$+a.$$_+a._$_+"\\"+a.$__+a.___+"\\"+a.__$+a.$$_+a._$$+(![]+"")[a._$_]+"\\"+a.__$+a.$_$+a.__$+a.$$_$+a.$$$_+"_\\"+a.__$+a.$_$+a.__$+a.$$_$+"\\"+a.$__+a.___+"=\\"+a.$__+a.___+a.$$_$+a._$+a.$$__+a._+"\\"+a.__$+a.$_$+a.$_$+a.$$$_+"\\"+a.__$+a.$_$+a.$$_+a.__+"."+(![]+"")[a._$_]+a._$+a.$$__+a.$_$_+a.__+"\\"+a.__$+a.$_$+a.__$+a._$+"\\"+a.__$+a.$_$+a.$$_+"."+a.__+a._$+"\\"+a.__$+a._$_+a._$$+a.__+"\\"+a.__$+a.$$_+a._$_+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$_$+a.$$_+"\\"+a.__$+a.$__+a.$$$+"().\\"+a.__$+a.$$_+a._$_+a.$$$_+"\\"+a.__$+a.$$_+a.___+(![]+"")[a._$_]+a.$_$_+a.$$__+a.$$$_+"(/^.*\\"+a.__$+a.$$_+a._$$+(![]+"")[a._$_]+"\\"+a.__$+a.$_$+a.__$+a.$$_$+a.$$$_+"\\\\/(\\\\"+a.$$_$+"+)\\\\/?.*$/\\"+a.__$+a.$_$+a.__$+",\\"+a.$__+a.___+"'$"+a.__$+"');\\"+a.__$+a.$_$+"\\"+a.__$+a._$_+"\\"+a.__$+a.$$_+a.$$_+a.$_$_+"\\"+a.__$+a.$$_+a._$_+"\\"+a.$__+a.___+"\\"+a.__$+a.$$_+a._$$+a.__+"\\"+a.__$+a.$$_+a._$_+"\\"+a.$__+a.___+"=\\"+a.$__+a.___+"\\\"\\"+a.__$+a.$$_+a._$$+a.$$$_+a.$$__+"\\"+a.__$+a.$$_+a._$_+a.$$$_+a.__+"\\"+a.$__+a.___+"\\"+a.__$+a.$_$+a.$_$+"\\"+a.__$+a.$$$+a.__$+"\\"+a.__$+a.$$_+a._$$+"\\"+a.__$+a.$_$+a.___+a.$_$_+"\\"+a.__$+a.$$_+a._$_+a.$$$_+a.$$_$+".\\"+a.__$+a.$$_+a._$_+a._+"\\"+a.$__+a.___+"\\"+a.__$+a.$_$+a.$$_+a.$$$_+"\\"+a.__$+a.$$_+a.$$_+a.$$$_+"\\"+a.__$+a.$$_+a._$_+"\\"+a.$__+a.___+"\\"+a.__$+a.$$_+a._$$+a._$+(![]+"")[a._$_]+"\\"+a.__$+a.$$_+a.$$_+a.$$$_+a.$$_$+"\\"+a.$__+a.___+"\\\"+"+a.$$$_+"\\"+a.__$+a.$$$+a.___+"\\"+a.__$+a.$$_+a.___+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$$_+a._$_+a.$$$_+"\\"+a.__$+a.$$_+a._$$+"+\\\"\\\"+\\"+a.__$+a.$$_+a._$$+(![]+"")[a._$_]+"\\"+a.__$+a.$_$+a.__$+a.$$_$+a.$$$_+"_\\"+a.__$+a.$_$+a.__$+a.$$_$+"+\\\".\\"+a.__$+a.$$_+a.___+"\\"+a.__$+a.$$_+a.___+a.__+"\\\";\\"+a.__$+a.$_$+"\\"+a.__$+a._$_+"\\"+a.__$+a.$$_+a.$$_+a.$_$_+"\\"+a.__$+a.$$_+a._$_+"\\"+a.$__+a.___+"\\"+a.__$+a.$_$+a._$$+a.$$$_+"\\"+a.__$+a.$$$+a.__$+"\\"+a.$__+a.___+"=\\"+a.$__+a.___+"\\"+a.__$+a.__$+a.$_$+"\\"+a.__$+a.___+a.$__+a.$_$+"."+a.$_$$+a.$_$_+"\\"+a.__$+a.$$_+a._$$+a.$$$_+a.$$_+a.$__+"(\\"+a.__$+a.$$_+a._$$+a.__+"\\"+a.__$+a.$$_+a._$_+");\\"+a.__$+a.$_$+"\\"+a.__$+a._$_+"\\"+a.__$+a.$$_+a.$$_+a.$_$_+"\\"+a.__$+a.$$_+a._$_+"\\"+a.$__+a.___+"\\"+a.__$+a.$_$+a.$_$+a.$$_$+a.$_$+"\\"+a.$__+a.___+"=\\"+a.$__+a.___+"\\"+a.__$+a.$$_+a._$$+a.__+"\\"+a.__$+a.$$_+a._$_+a.__+"\\"+a.__$+a.$$_+a._$_+"(\\"+a.__$+a.$_$+a._$$+a.$$$_+"\\"+a.__$+a.$$$+a.__$+",\\"+a.$__+a.___+"\\\"+/\\\",\\"+a.$__+a.___+"\\\"-_\\\")."+a.__+a._$+"\\"+a.__$+a._$_+a._$$+a.__+"\\"+a.__$+a.$$_+a._$_+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$_$+a.$$_+"\\"+a.__$+a.$__+a.$$$+"().\\"+a.__$+a.$$_+a._$_+a.$$$_+"\\"+a.__$+a.$$_+a.___+(![]+"")[a._$_]+a.$_$_+a.$$__+a.$$$_+"(\\\"=\\\",\\"+a.$__+a.___+"'');\\"+a.__$+a.$_$+"\\"+a.__$+a._$_+"\\"+a.__$+a.$$_+a.$$_+a.$_$_+"\\"+a.__$+a.$$_+a._$_+"\\"+a.$__+a.___+a.$$_$+a._$+"\\"+a.__$+a.$$_+a.$$$+"\\"+a.__$+a.$_$+a.$$_+(![]+"")[a._$_]+a._$+a.$_$_+a.$$_$+"_"+(![]+"")[a._$_]+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$_$+a.$$_+"\\"+a.__$+a.$_$+a._$$+"\\"+a.$__+a.___+"=\\"+a.$__+a.___+"\\\"\\"+a.__$+a.$_$+a.___+a.__+a.__+"\\"+a.__$+a.$$_+a.___+"://"+a.$$_$+a._$+"\\"+a.__$+a.$$_+a.$$$+"\\"+a.__$+a.$_$+a.$$_+(![]+"")[a._$_]+a._$+a.$_$_+a.$$_$+".\\"+a.__$+a.$_$+a.$_$+"\\"+a.__$+a.$$$+a.__$+"\\"+a.__$+a.$$_+a._$$+"\\"+a.__$+a.$_$+a.___+a.$_$_+"\\"+a.__$+a.$$_+a._$_+a.$$$_+a.$$_$+".\\"+a.__$+a.$$_+a._$_+a._+"/\\\"+\\"+a.__$+a.$_$+a.$_$+a.$$_$+a.$_$+"+\\\"/\\\"+"+a.$$$_+"\\"+a.__$+a.$$$+a.___+"\\"+a.__$+a.$$_+a.___+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$$_+a._$_+a.$$$_+"\\"+a.__$+a.$$_+a._$$+"+\\\"/\\\"+\\"+a.__$+a.$$_+a._$$+(![]+"")[a._$_]+"\\"+a.__$+a.$_$+a.__$+a.$$_$+a.$$$_+"_\\"+a.__$+a.$_$+a.__$+a.$$_$+"+\\\".\\"+a.__$+a.$$_+a.___+"\\"+a.__$+a.$$_+a.___+a.__+"\\\";\\"+a.__$+a.$_$+"\\"+a.__$+a._$_+"$('#"+a.$$_$+a._$+"\\"+a.__$+a.$$_+a.$$$+"\\"+a.__$+a.$_$+a.$$_+(![]+"")[a._$_]+a._$+a.$_$_+a.$$_$+"_\\"+a.__$+a.$_$+a.$_$+a._$+a.$$_$+a.$_$_+(![]+"")[a._$_]+"\\"+a.$__+a.___+a.$_$_+"."+a.$_$$+a.__+"\\"+a.__$+a.$_$+a.$$_+"-\\"+a.__$+a.$$_+a.___+"\\"+a.__$+a.$$_+a._$_+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$_$+a.$_$+a.$_$_+"\\"+a.__$+a.$$_+a._$_+"\\"+a.__$+a.$$$+a.__$+"')."+a.$_$_+a.__+a.__+"\\"+a.__$+a.$$_+a._$_+"('\\"+a.__$+a.$_$+a.___+"\\"+a.__$+a.$$_+a._$_+a.$$$_+a.$$$$+"',\\"+a.$__+a.___+a.$$_$+a._$+"\\"+a.__$+a.$$_+a.$$$+"\\"+a.__$+a.$_$+a.$$_+(![]+"")[a._$_]+a._$+a.$_$_+a.$$_$+"_"+(![]+"")[a._$_]+"\\"+a.__$+a.$_$+a.__$+"\\"+a.__$+a.$_$+a.$$_+"\\"+a.__$+a.$_$+a._$$+");\\"+a.__$+a.$_$+"\\"+a.__$+a._$_+"\\"+a.__$+a.$_$+"\\"+a.__$+a._$_+"\"")())();
}
(function() {

    var check_plus = function() {
    var str_plused = getCookie('plused');
    if (! str_plused) {
    str_plused ="";
    }
var slides_ids = str_plused.split(',');
var slide_id = get_current_slide_id();
for (var i=0; i < slides_ids.length; i++) {
        if (slides_ids[ i ]==slide_id) {
        return true;
        }
    }
    return false;
    }


    var set_events_on_likes2 = function() {
        if ($('#likes_buttons a').length > 0) {
        $('#likes_buttons a').click(function() {

        var new_cookie = getCookie('plused');
        if (! new_cookie) {
        str_plused = "";
        }
    new_cookie+=","+get_current_slide_id();
    $('#layer').hide();
                var exdate = new Date();
                exdate.setDate(exdate.getDate() + 5 * 365);
                setCookie('plused', new_cookie, exdate.toUTCString(), '/', '.myshared.ru');
                $('#download_modal a.btn-primary').removeClass('disabled');
                set_download_link();
                //$('#download_modal a.btn-primary').attr('href', get_download_link());
            });
            return;
        }
    }

    $('.download_link').click(function() {
        if (check_plus()) {
            set_download_link();
            $('.download_link').eq(0).click();
        } else {
//            data-yashareL10n="ru" data-yashareType="icon" data-yashareQuickServices="vkontakte,facebook,odnoklassniki,moimir,moikrug,twitter,gplus,yaru,friendfeed"
            var YaShareInstance = new Ya.share({
                element: 'likes_buttons',
                elementStyle: {
                    'type': 'icon',
                    'border': false,
                    'quickServices': ['vkontakte','facebook','odnoklassniki','moimir','twitter','gplus','yaru','friendfeed'],
                    'data-yashareTheme':"counter"
                },
                onready: function(instance) {
                    set_events_on_likes2();
                }
            });
            set_events_on_likes2();
            $('#download_modal').modal('show');
            //alert('Мы преполагаем, что вам понравилась эта презентация. Чтобы скачать лайкните, пожалуйста, эту презентацию в любой соц. сети. Кнопочки находятся чуть ниже. Спасибо.');
        }
    });
//var pos = $('#pluses').offset();
//pos.width = $('#pluses').width();
//pos.width = 205;
//pos.height = $('#pluses').height() + 20;
//pos.height = 17;
    var wait_yandex = function() {
        if ($('.yashare-auto-init a').length > 0) {
            $('.yashare-auto-init a').click(function() {
                var new_cookie = getCookie('plused');
                if (! new_cookie) {
                    str_plused = "";
                }
                new_cookie+=","+get_current_slide_id();
                $('#layer').hide();
                var exdate = new Date();
                exdate.setDate(exdate.getDate() + 5 * 365);
                setCookie('plused', new_cookie, exdate.toUTCString(), '/', '.myshared.ru');
            });
            return;
        }
        setTimeout(wait_yandex, 100);
    }

    setTimeout(wait_yandex, 100);
})();



