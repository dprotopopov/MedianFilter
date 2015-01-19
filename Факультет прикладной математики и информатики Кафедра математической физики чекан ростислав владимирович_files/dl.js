var shprB=0;function shpr(s0,s1){var o=document.getElementById("shprd");if(shprB){shprB=0;o.style.visibility="hidden";o.style.display="none";return;}
shprB=1;o2=document.getElementById("mtable");if((o!=null)&&(o2!=null)){o.style.visibility="visible";o.style.display="block";
a=document.getElementsByTagName('title');if(a!=null){n=a[0].innerHTML;n=n.replace(new RegExp(' {1,}$'),'');s='<h2>'+n+'</h2>';}else n='';
r=o2.innerHTML.match(new RegExp('.{0,600}?([А-ЯA-Z][А-ЯA-Za-zа-яіїєёІЇЄЁ0-9()\'"!?;:., -]{128,1200}[.!])',''));
if(r!=null)s=s+r[1];s=s+'<br />'+s1+'<br />';
o.innerHTML=s0+'<br /><textarea class="shprt">'+s+'</textarea>';}}
var imgbg=null,u=5,starsp=Array(),xh=null,com_sn=null,com_te=null,code=null;function ste(o){var x=0,y=0;while(o!=null){x+=o.offsetLeft;y+=o.offsetTop;o=o.offsetParent;}window.scrollTo(x,y);}
function goback(id){o=document.getElementById("back-"+id);if(o!=null){ste(o);}}
function init(){for(i=1;i<=6;i++){starsp[i]=document.getElementById("star"+i);}
com_sn=document.getElementById("com_sn");com_te=document.getElementById("com_te");com_t=document.getElementById("com_t");}
var old_data=Array();function highlight(k)
{try{if(code==null)code=document.getElementsByTagName('ST');k=k+' ';k=k.replace(/(.+(\s|\b))\1+/gi,'$1');k2='';while((k!=k2)&&(k!='')){if(k2!='')k=k2;k2=k.replace(/( |^)[a-zа-яієї]{1,2}( |$)/gi,' ');}
k=k.replace(/(\s+$)|(^\s+)/gi,'');k=k.replace(/[$!,.-]/gi,' ');k=k.replace(/\s+/gi,' ');k=k.replace(/\s/gi,'|');k=k.replace(/[*]/gi,'');if((k.length>2)&&(code!=null))for(i=0,l=code.length;i<l;i++)
{if(old_data[i]==null)old_data[i]=code[i].parentNode.innerHTML;else code[i].parentNode.innerHTML=old_data[i];wrapWordsInDescendants(code[i].parentNode,'font','highlight',new RegExp('('+k+')','gi'));}}catch(err){}}
function wrapWordsInDescendants(element,tagName,className,k){for(var i=element.childNodes.length;i-->0;){var child=element.childNodes[i];if(child.nodeType==1)
wrapWordsInDescendants(child,tagName,className,k);else if(child.nodeType==3)
wrapWordsInText(child,tagName,className,k);}}
function wrapWordsInText(node,tagName,className,k){var ixs=[];var match;var $i=0;while(match=k.exec(node.data)){ixs.push([match.index,match.index+match[0].length]);$i++;if($i>300)break;}
for(var i=ixs.length;i-->0;){var element=document.createElement(tagName);element.className=className;node.splitText(ixs[i][1]);element.appendChild(node.splitText(ixs[i][0]));node.parentNode.insertBefore(element,node.nextSibling);}}
function show_sc(o){xh=GetXmlHttpObject();if(xh==null)return;var url="/rudocs/show_sc.php?c="+parseInt(o.value);xh.onreadystatechange=ch_sc;xh.open("GET",url,true);xh.setRequestHeader('Content-Type','application/x-www-form-urlencoded');xh.send('');}
function ch_sc()
{if(xh.readyState==4){if(xh.responseText!='0'){o=document.getElementById('sc1');if(o!=null)o.innerHTML=xh.responseText;}}}
function sendcom(id){if((com_sn==null)||(com_te==null))return;try{if(xh!=null){xh.onreadystatechange=null;xh.abort;xh.close;}}catch(err){}
xh=GetXmlHttpObject();if(xh==null)return;var url="/rudocs/addcomm.php?id="+id+"&t="+parseInt(com_t.value);xh.onreadystatechange=sch2;xh.open("POST",url,true);xh.setRequestHeader('Content-Type','application/x-www-form-urlencoded');xh.send('te='+com_te.value+'&sn='+com_sn.value);}
function sch2()
{if(xh.readyState==4){if(xh.responseText!='0'){o=document.getElementById('comments1');if(o!=null)o.innerHTML=xh.responseText;}}}
function sch()
{if(xh.readyState==4){if(xh.responseText!='0'){a=xh.responseText.match(/r[1-6]=([0-9]*);r[1-6]=([0-9]*);r[1-6]=([0-9]*);r[1-6]=([0-9]*);r[1-6]=([0-9]*);r[1-6]=([0-9]*);/);r2=a[1]+a[2]+a[3]+a[4]+a[5];if(r2>0){m=1;for(i=1;i<=5;i++){if(m<a[i])m=parseInt(a[i]);}
for(i=1;i<=5;i++)if(a[i]>0){w=Math.round((a[i]/m)*150);o=document.getElementById('ratebar'+i);if(o!=null){o.style.visibility="visible";o.style.position="relative";}
o=document.getElementById('ratebarw'+i);if(o!=null)o.style.width=(w+60)+"px";o=document.getElementById('ratebarf'+i);if(o!=null)o.innerHTML=a[i];}}}}}
function rateth(id,l){if(starsp[l]==null)return;try{if(xh!=null){xh.onreadystatechange=null;xh.abort;xh.close;}}catch(err){}
xh=GetXmlHttpObject();if(xh==null)return;var url="/rudocs/ratethis.php?id="+id+'&r='+l;xh.open("POST",url,true);xh.setRequestHeader('Content-Type','application/x-www-form-urlencoded');xh.onreadystatechange=sch;xh.send('');setCookie('rate'+l0+'_'+id,l,1);if(l==6)l0=l;else{l0=1;l=5;}
for(i=l0;i<=l;i++){starsp[i]=null;}}
function ratep(l,op){if(l==6)l0=l;else l0=1;for(i=l0;i<=l;i++)if(starsp[i]!=null){starsp[i].style.opacity=op;starsp[i].style.filter='alpha(opacity='+Math.round(op*100)+')';}}
function mkbig(o,i,mw,mh,s){u=5;imgbg=document.getElementById("imgbig"+i);imgbg.width=o.width;imgbg.src=o.src;imgbg.style.visibility="visible";if(s!='')imgbg.src=s;a=findPos(o);imgbg.style.left=a[0]+"px";imgbg.style.top=a[1]+"px";d=(screen.width-(a[0]-o.width))/(a[0]-10);d2=(screen.width-(a[0]-o.width))/(a[1]-20);setTimeout('bg('+a[0]+','+a[1]+','+o.width+','+o.height+','+d+','+d2+',1,'+a[0]+','+mw+','+mh+')',10);imgbg.onclick=function(){mksm(o,i,a[0]);};}
function mksm(o,i,ox){u=5;imgbg=document.getElementById("imgbig"+i);x=parseInt(imgbg.style.left);y=parseInt(imgbg.style.top);a=findPos(o);d=(screen.width-(a[0]-o.width))/(a[0]);d2=(screen.width-(a[0]-o.width))/(a[1]-20);bg(x,y,imgbg.width,imgbg.height,d,d2,-1,ox);}
function bg(x,y,w,h,d,d2,n,ox,mw,mh){if(imgbg==null)return;if(u<50)u+=2;if((x-(u*n))<10){u=x-10;bb=1;}else bb=0;x-=u*n;y-=(u*n)/d2;w+=(u*n)/d;if(((x>=10)&&(imgbg.height<mh)&&(imgbg.width<mw)&&(y+imgbg.height<screen.height-100)&&(n>0))||((x<ox)&&(n<0))){imgbg.style.left=x+"px";imgbg.style.top=y+"px";imgbg.width=w;if(!bb)setTimeout('bg('+x+','+y+','+w+','+h+','+d+','+d2+','+n+','+ox+','+mw+','+mh+')',10)}else{if(n<0)imgbg.style.visibility="hidden";}}
function findPos(obj){var curleft=curtop=0;if(obj.offsetParent){do{curleft+=obj.offsetLeft;curtop+=obj.offsetTop;}while(obj=obj.offsetParent);return[curleft,curtop];}else{if(obj.x)curleft+=obj.x;if(obj.y)curtop+=obj.y;}}
function GetXmlHttpObject(){if(window.XMLHttpRequest)return new XMLHttpRequest();if(window.ActiveXObject)return new ActiveXObject("Microsoft.XMLHTTP");return null;}
function setCookie(c_name,value,exdays)
{var exdate=new Date();exdate.setDate(exdate.getDate()+exdays);var c_value=escape(value)+((exdays==null)?"":"; expires="+exdate.toUTCString());document.cookie=c_name+"="+c_value;}