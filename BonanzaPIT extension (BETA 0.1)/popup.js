// Copyright (c) 2012 The Chromium Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

//'use strict';

var listString = "";
var bg = chrome.extension.getBackgroundPage();


function click(e) {
  if(e.target.id === "get-all")
  {
	chrome.tabs.executeScript(null, {code: "GetProductsFromThisPage();"});
	chrome.tabs.executeScript(null, {code: "SendDataToBackground();"});
  }
  if(e.target.id === "this-product")
  {
	  
	  chrome.tabs.getSelected(null, function(tab){
		  if(tab.url.includes('aliexpress.com/item'))
		  {
				chrome.tabs.executeScript(null, {code: "globalString += " + JSON.stringify(tab.url) + " + ' '; 	productCount++; SendDataToBackground();"});
		  }
	  });
	  
  }
  if(e.target.id === "download")
  {
	  var date = new Date().toLocaleString();
	  bg.DownloadFile(JSON.stringify(date));
  }
  if(e.target.id == "start-new")
  {
	  bg.b_list = [];
	  bg.b_count = 0;
	  bg.UpdateBadge();
  }
  window.close();
}

document.addEventListener('DOMContentLoaded', function () {
  var divs = document.querySelectorAll('div');
  for (var i = 0; i < divs.length; i++) {
    divs[i].addEventListener('click', click);
  }
  	 SetSpanText(); 
});

function SetSpanText(){
	var spanCount = document.getElementById('list-count');
	spanCount.innerText = bg.b_count.toString();
}
