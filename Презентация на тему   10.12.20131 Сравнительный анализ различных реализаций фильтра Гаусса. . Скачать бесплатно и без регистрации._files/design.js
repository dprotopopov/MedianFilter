var api = {
    default_speed: 40,
    speed: 40,
    play: 0,
    last_clicked_slide: 0,
    current_slide_clicks: 0,
    wait_between_slides: 3000,
    lock_wait: 0,
    is_playing: function() {
        return $('button.play_button').hasClass('selected');
    },
    progressbar_timeout: 100
};

var img_play_64 = "data:image/png;base64,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";

$(document).ready(function() {



    var init_api = function() {

        var core = yes;
        api.core = yes;
        api.total_slides = api.core.W.Ca.$D.P.P.length - 1;

        api.play_start = api.core.W.play;
        api.slide_text_length = function(i) {
            return api.core.W.sh.ph[i].Oc.textContent.toString().length;
        };
        api.current_slide = function() {
            return api.core.W.O;
        }
        api.slide_frames = function(i) {
            return yes.W.Ca.$D.P.P[i].Aj.wc.m.s.length;
        };

        api.wait_next_slide = function(){
            api.lock_wait = 1;
            //console.log('sleep 2 seconds');

            var timeout = api.slide_text_length(api.current_slide()) * api.speed;

            if (timeout < api.wait_between_slides) {
                timeout = api.wait_between_slides;
            }

            setTimeout("api.lock_wait=0;api.core.W.play();", timeout);

            progress_bar(api.current_slide(), 0, timeout);
        };

        api.show_progressbar = function(passed, total, slide) {
            if (passed>=total) {
                return;
            }
            var percent = Math.ceil(passed / total * 100);
            $('#bpbar').css('width', percent.toString()+'px');
        }
    }();
    var beautify_player = function() {
        var bar = $('div.statusbar').eq(1);
        var html = '<table style="margin-top: 7px;"><tr>';
        html+= '<td><img src="data:image/png;base64,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" action="start"></td>';
        html+= '<td><img src="data:image/png;base64,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" action="back"></td>';
        html+= '<td><img src="'+img_play_64+'" action="play" id="but_play"></td>';
        html+= '<td><img src="data:image/png;base64,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" action="forward">';
        html+= '</td><td><img src="data:image/png;base64,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" action="end"></td>';
        html+= '</tr></table>';

        $('.navigation_panel').eq(0).prepend('<div style="position:absolute; top:0px; left:0px; width:100%; height:5px;z-index:1000;"><div id="bpbar" style="background: navy;height:5px;width:0%;"></div></div>');
        var o = $('div.control_panel').eq(0);
        o.css('overflow', 'hidden');
        o.css('width', '175px');
        o.find('div').hide();
        o.prepend(html);
        o.find('img[action]').hover(function(){
            $(this).css('opacity', '0.5');
        }, function(){
            $(this).css('opacity', '1');
        });
        o.find('img[action]').click(function(){
            switch($(this).attr('action')){
                case 'start': api.core.gotoFirstSlide(); break;
                case 'end':api.core.gotoLastSlide(); break;
                case 'back': api.core.gotoPreviousSlide(); break;
                case 'forward': api.core.gotoNextSlide(); break;
                case 'play': play_click(); break;
                case 'pause': pause_click(); break;
            }
        });
        $('.navigation_panel').css('overflow', 'hidden');
        bar.find('div.progressbar').remove();
        $('div.statusbar').eq(1).find('div > div.label').eq(0).css('left', '130px').css('top', '0px').css('width', '100px').css('text-align', 'left');
        bar.find('div > div.label').eq(1).remove(); // таймер
        bar.append('<div id="tt" style="z-index: 9; position: absolute; left:0px; top: -8px; height: 30px; overflow:hidden;width: 120px;"><a href="http://www.myshared.ru/" target="_blank"><img src="data:image/png;base64,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" border=0 width=120 height=30></a></div>');
        $('div.label.component_container').eq(1).remove()
        $('#myloading').remove();
    }();

    var clicking_play = function() {
        if (api.lock_wait || api.play==0 || api.current_slide() == api.total_slides) {
            return;
        }
        var is_playing = $('button.play_button').hasClass('selected');
        if (is_playing) { // плеер что-то показывает в данный момент
            return;
        } else {
            if (api.current_slide() != api.last_clicked_slide) {
                api.current_slide_clicks = 0;
            }
            api.last_clicked_slide = api.current_slide();



            var total_clicks = api.slide_frames(api.last_clicked_slide);
            api.current_slide_clicks++;
//        console.log('current slide:', api.last_clicked_slide, 'total_clicks:', total_clicks, 'current_slide_clicks:', api.current_slide_clicks, 'api.lock_wait:', api.lock_wait);

            if (total_clicks <= api.current_slide_clicks) {
                api.wait_next_slide();
                return;
            }
            api.core.W.play();
        }
    };
    setInterval(clicking_play, 200);

//    var check_int_play = function() {
//        console.log(api.play, $('button.play_button').hasClass('selected'));
//        if (!api.play && $('button.play_button').hasClass('selected')) {
//            $('button.play_button').click();
//        }
//    };
//    check_int_play();
//    setInterval(check_int_play, 1000);
    if (out = document.location.toString().match(/slide=(\d+)/)) {
        if (api.total_slides >= out[1]-1) {
            api.core.gotoSlide(out[1]-1);
        }
    }


});

function draw_speed_play() {
    return;
    $('div.label.component_base').append('<span style="margin-left:20px; display: none;" id="speed_play"><span speed="0.5">0.5x</span> <span speed="1">1x</span> <span speed="2">2x</span> <span speed="3">3x</span></span>');
    var user_speed = getCookie('speed_play') * 1;
    api.speed = api.default_speed * (user_speed + 1);
    $('#speed_play span[speed="'+user_speed+'"]').css('font-weight', '900');
    $('#speed_play span').click(function(){
        $('#speed_play span').css('font-weight', '100');
        $(this).css('font-weight', '900');
        var speed = $(this).attr('speed');
        setCookie('speed_play', speed);
        api.speed = api.default_speed * speed;
    });
}

function loop_play() {
    if (!api.play) {
        return;
    }
    var cur_slide = api.current_slide();
    if (cur_slide == api.total_slides) {
        pause_click();
        return;
    }
//    console.log(cur_slide);
    var x = $('button.play_button').css('background').toString().match(/4NCjwvc3ZnPg0K\)/);
    if (x) { // плеер что-то показывает в данный момент
        window.setTimeout(loop_play, 100);
        return;
    }
    slide_time();
}

function slide_time() {
    var timeout = api.slide_text_length(api.current_slide()) * api.speed;
    if (timeout < 2000) {
        timeout = 2000;
    }
    show_progressbar(api.current_slide(), 0, timeout);
}


function show_progressbar(slide, now, total) {
    return;
    //console.log('ok');
    if (slide!=api.current_slide()) {
        return;
    }
    var x = $('button.play_button').css('background').toString().match(/4NCjwvc3ZnPg0K\)/);
    if (x) { // плеер что-то показывает в данный момент
        $('#bpbar').css('width', '0px');
        window.setTimeout("show_progressbar("+slide+","+(now)+", "+total+")", 10);
        return;
    }

    if (!api.play) {
        $('#bpbar').css('width', '0px');
        return;
    }
    var percent = Math.ceil(now / total * 100);
    if (percent >= 100) {
        $('#bpbar').css('width', '0px');
        $('button.play_button').click();
        loop_play();
        return;
    }
    $('#bpbar').css('width', percent+'%');
    window.setTimeout("show_progressbar("+slide+","+(now+10)+", "+total+")", 10);
}


function play_click() {
    api.play = 1;
    $('#but_play').attr('src', 'data:image/png;base64,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');
    $('#but_play').attr('action', 'pause');
    $('#speed_play').show();
//    $('button.play_button').click();
}




var progress_bar = function(slide, now, total) {
    if (slide!=api.current_slide()) {
        $('#bpbar').hide();
        return;
    }
//    console.log(slide, now, total);
    if (api.lock_wait == 0) { // идет анимация
        if ($('#bpbar:visible')) {
            $('#bpbar:visible').hide();
        }
        return;
    }
    $('#bpbar:hidden').show();

    var percent = Math.ceil(now / total * 100);
    if (percent > 100) {
        percent = 100;
    }
    $('#bpbar').css('width', percent+'%');
    now = now + api.progressbar_timeout;
    setTimeout("progress_bar("+slide+", "+now+", "+total+");", api.progressbar_timeout);
};


//setInterval(progress_bar, 200);


function pause_click() {
    api.play = 0;
    $('#speed_play').hide();
    $('#but_play').attr('src', img_play_64);
    $('#but_play').attr('action', 'play');
}

