/**
 *
 * http://tech-algorithm.com
 * 
 */
 
usingIE = (navigator.appName == 'Microsoft Internet Explorer')?true:false ;

if (document.attachEvent) {    
  document.attachEvent('onclick',mouseClicked) ;
} else {  
  document.addEventListener('click',mouseClicked,false) ;  
}

function stopEvent(evt) {
  if (evt.preventDefault) {
    evt.preventDefault() ;
  } else {
    evt.returnValue = false ; // teh sucky IE way...
  }
}

function mouseClicked(evt) {    
  e=evt.target?evt.target:evt.srcElement ;                                             
  if (e.tagName=='A' && (actformtag=document.getElementById('mod-form'))) {    
    if (e.id=='mod-delete') {                  
      stopEvent(evt) ;      
      actformtag.modtype.value = 'delete' ;
      actformtag.submit() ;
    } else if (e.id=='mod-accept') {
      stopEvent(evt) ;
      actformtag.modtype.value = 'accept' ;
      actformtag.submit() ;
    }
  }
}    