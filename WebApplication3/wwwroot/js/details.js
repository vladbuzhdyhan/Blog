let plainTextData = document.getElementById("my-data").value;
let output = document.getElementById("output");
output.contentDocument.body.innerHTML = plainTextData;

var iframeDocument = output.contentDocument || output.contentWindow.document;
var style = iframeDocument.createElement('style');
style.textContent = `
   * {
    font-family: sohne, "Helvetica Neue", Helvetica, Arial, sans-serif;
    font-size: 16px;
   }
`;
iframeDocument.head.appendChild(style);

const iframeBody = output.contentDocument.body;
output.style.height = iframeBody.scrollHeight + "px";