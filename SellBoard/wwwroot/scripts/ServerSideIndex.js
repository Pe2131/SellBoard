var t;
function setClock(t){
	//var t = new Date();
	t.setSeconds( t.getSeconds() + 1);
  // I know its ugly... but make it better and this efficient ;)
  if(t.getHours() > 9){ 
  	$('.clock .digit:nth-child(1) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getHours()+"").split("")[0]);
		$('.clock .digit:nth-child(2) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getHours()+"").split('')[1])
	} else { 
		$('.clock .digit:nth-child(1) .pixel').removeClass().addClass('pixel').addClass('_0'); 
		$('.clock .digit:nth-child(2) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getHours()+"").split('')[0]) 
	}

if(t.getMinutes() > 9){$('.clock .digit:nth-child(4) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getMinutes()+"").split("")[0]); $('.clock .digit:nth-child(5) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getMinutes()+"").split('')[1]) } else { $('.clock .digit:nth-child(4) .pixel').removeClass().addClass('pixel').addClass('_0'); $('.clock .digit:nth-child(5) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getMinutes()+"").split('')[0]) }

if(t.getSeconds() > 9){ $('.clock .digit:nth-child(7) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getSeconds()+"").split("")[0]); $('.clock .digit:nth-child(8) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getSeconds()+"").split('')[1]) } else {	$('.clock .digit:nth-child(7) .pixel').removeClass().addClass('pixel').addClass('_0'); $('.clock .digit:nth-child(8) .pixel').removeClass().addClass('pixel').addClass('_'+(t.getSeconds()+"").split('')[0]) } 
			}

$(document).ready(function(){
	t=new Date();
		var SumOfeffectiveCalls=0;
	var SumofCards=0;
	var SumOfFtd=0;
	t.setHours(0,0,0,0);
	setClock(t);
	window.setInterval(function(){setClock(t)},1000);
//	window.setTimeout( function(){location.reload();},10000);
	$(document).ready(function() {
		$.getJSON("http://10.30.30.220:9001/sales/websalestv/getsums?branchid=1&DepartmentGroupId=2&FromDate=2019-04-01", function(res) {
			// alert(res.Message[0]);
			var s= JSON.stringify(res);
			var jsonData = JSON.parse(s);
			console.log(jsonData);
			//console.log(s.message.length);			
for (var i = 0; i < 10; i++) {
    var counter = jsonData.Message[i];
	var j=i+1;
   // console.log(counter);
	//	alert(counter.StageName);
			 var item = "<tr id='tr_"+j+"'><td id='td_"+j+"'>" + j + "</td><td>"+counter.StageName+"</td><td>"+counter.EffectiveCalls+"</td><td>"+counter.DepositAttempts+"</td><td>"+counter.ConfirmedDeposits+"</td></tr>";
			 	 $("#list").append(item);
				 	 	 SumOfeffectiveCalls+=counter.EffectiveCalls;
	 SumofCards+=counter.DepositAttempts;
	 SumOfFtd+= counter.ConfirmedDeposits;
	}			
$("#firstLabale").append(SumOfeffectiveCalls);
$("#SecLabale").append(SumofCards);
$("#therdLabale").append(SumOfFtd);
});

});
});