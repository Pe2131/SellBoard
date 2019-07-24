function GetFirstOfMonth() {
	var today = new Date();
	var formattedfirstday = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + 1;
	return formattedfirstday;
}
function GetTodayDate() {
		var today = new Date();   
		var formattedtoday =  today.getFullYear() + '-' + (today.getMonth() + 1) + '-' +today.getDate() ;
    return formattedtoday;
}
function setClock(t){
	//var t = new Date();
	t.setSeconds( t.getSeconds() + 1);
	// for set 0 to day
	$('.clock .digit:nth-child(1) .pixel').removeClass().addClass('pixel').addClass('_0'); 
	$('.clock .digit:nth-child(2) .pixel').removeClass().addClass('pixel').addClass('_0') ;
  // I know its ugly... but make it better and this efficient ;)
  if(t.getHours() > 9){ 
  	$('.clock .digit:nth-child(3) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getHours()+"").split("")[0]);
		$('.clock .digit:nth-child(4) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getHours()+"").split('')[1])
	} else { 
		$('.clock .digit:nth-child(3) .pixel').removeClass().addClass('pixel').addClass('_0'); 
		$('.clock .digit:nth-child(4) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getHours()+"").split('')[0]) 
	}

if(t.getMinutes() > 9){$('.clock .digit:nth-child(6) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getMinutes()+"").split("")[0]); $('.clock .digit:nth-child(7) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getMinutes()+"").split('')[1]) } else { $('.clock .digit:nth-child(6) .pixel').removeClass().addClass('pixel').addClass('_0'); $('.clock .digit:nth-child(7) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getMinutes()+"").split('')[0]) }

if(t.getSeconds() > 9){ $('.clock .digit:nth-child(9) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getSeconds()+"").split("")[0]); $('.clock .digit:nth-child(10) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getSeconds()+"").split('')[1]) } else {	$('.clock .digit:nth-child(9) .pixel').removeClass().addClass('pixel').addClass('_0'); $('.clock .digit:nth-child(10) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getSeconds()+"").split('')[0]) } 
			}
function EmptyElement() {
	$('#list tbody').empty();
	$("#numberOftable").empty();
	$('#titleOfTable').empty();
	$("#firstLabale").empty();
	$("#SecLabale").empty();
	$("#therdLabale").empty();
	$("#MonthlySales-Conversion tbody").empty();
	$("#FLMCsum").empty();
	$("#SLMCsum").empty();
	$('#DailySales-Retention tbody').empty();
	$("#FLDRsum").empty();
	$("#SLDRsum").empty();
	$("#TLDRsum").empty();
	$('#MonthlySales-Retention tbody').empty();
	$("#FLMRsum").empty();
	$("#SLMRsum").empty();
}
function ShowCorrectTable(table,foter) 
{
	$('#list').hide();
	$('#Daily').hide();
	$('#DailySales-Retention').hide();
	$('#DailySales-RetentionFooter').hide();
	$('#MonthlySales-Retention').hide();
	$('#MonthlySales-RetentionFooter').hide();
	$('#MonthlySales-Conversion').hide();
	$('#MonthlySales-ConversionFooter').hide();
	$('#'+table).show();
	$('#'+foter).show();
}
function formatMoney(amount, decimalCount = 2, decimal = ".", thousands = ",") {
	try {
	  decimalCount = Math.abs(decimalCount);
	  decimalCount = isNaN(decimalCount) ? 2 : decimalCount;
  
	  const negativeSign = amount < 0 ? "-" : "";
  
	  let i = parseInt(amount = Math.abs(Number(amount) || 0).toFixed(decimalCount)).toString();
	  let j = (i.length > 3) ? i.length % 3 : 0;
  
	  return negativeSign + (j ? i.substr(0, j) + thousands : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands) + (decimalCount ? decimal + Math.abs(amount - i).toFixed(decimalCount).slice(2) : "");
	} catch (e) {
	  console.log(e)
	}
  }
function DifrenceTime(mydate) 
{
	var now= new Date();
	var lastdate=new Date(mydate);
	console.log('Now:'+now);
	console.log(lastdate);
	var diff = now - lastdate;
	var diffinHour=Math.floor(diff/1000/60/60); // Convert milliseconds to Hour	
	var day=Math.floor(diffinHour/24);
	var hour=Math.floor(diffinHour%24);
	var Min =GetMin(diff/1000/60); // Convert milliseconds to Minutes	
	console.log(day+'-'+hour+'-'+Min);
	ResetTime2(day,hour,Min);
}
 function GetMin (minutes) 
 {
	var m = Math.floor(minutes % 60);
  return m
}
 function compareLastdateTime(lastSalC,lastsalR)
{
		var C= new Date(lastSalC);
		var R= new Date(lastsalR);
		var diff = C.valueOf() - R.valueOf();
		if(diff>=0)
		{
      return true;
		}
		return false;
}