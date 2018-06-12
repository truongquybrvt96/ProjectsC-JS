var productCount = 0;
var globalString = [];
function click(e){
	
	globalString.push(e.target.getAttribute('the-link'));
	productCount++;
	SendDataToBackground();
}



var pics = document.getElementsByClassName('pic');
for(var i = 0; i < pics.length; i++)
{
	//Product link
	var productLink;
	for(var h = 0; h < pics[i].children.length; h++)
	{
		if(pics[i].children[h].tagName == 'A')
		{
			productLink = pics[i].children[h].getAttribute('href');
		}
	}
	if(productLink === undefined || productLink === null)
		continue;
	
	var itemChildren = pics[i].parentElement.parentElement.children;
	var aElement = document.createElement('a');
	for(var j = 0; j < itemChildren.length; j++)
	{
		if(itemChildren[j].getAttribute("class") == "info")
		{
			var imgElement = document.createElement('img');
			imgElement.setAttribute('src', 'chrome-extension://jmjddlfanhofbgopfbjbmipipnpifaab/icon48.png');
			imgElement.setAttribute('width', '30px');
			imgElement.setAttribute('height', '30px');
			imgElement.setAttribute('the-link', productLink);
			aElement.appendChild(imgElement);
			aElement.addEventListener('click', click);
			itemChildren[j].appendChild(aElement);
		}
	}
}


	
function GetProductsFromThisPage(){
	var pics = document.getElementsByClassName('pic');
	for(var i = 0; i < pics.length; i++)
	{
		for(var h = 0; h < pics[i].children.length; h++)
		{
			if(pics[i].children[h].tagName == 'A')
			{
				var aHref = pics[i].children[h].getAttribute('href');
				globalString.push(aHref);
				productCount++;
			}
		}
	}
}

function SendDataToBackground(){
	chrome.runtime.sendMessage({counter: productCount, listOfString: globalString, key: 'data'});
	productCount = 0;
	globalString = [];
}