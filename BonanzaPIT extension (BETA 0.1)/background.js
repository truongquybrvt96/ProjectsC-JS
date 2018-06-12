var b_list = [];
var b_count = 0;
chrome.runtime.onMessage.addListener(function(mess, sender, sendResponse){
		if(mess.key == 'data')
		{
			b_count += mess.counter;
			b_list = b_list.concat(mess.listOfString);
			UpdateBadge();
		}
});

function DownloadFile(filename)
{
	saveText(filename, b_list.join(' '));
}

function saveText(filename, text) {
	var tempElem = document.createElement('a');            
	tempElem.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));            tempElem.setAttribute('download', filename);            
	tempElem.click();         
	}

function UpdateBadge()
{
	chrome.browserAction.setBadgeText({text: b_count.toString()});
}

