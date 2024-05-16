let gr = null;

window.chrome.webview.addEventListener('message', event => {
    WriteDataFromCsharp(event.data);
});

function WriteDataFromCsharp(data) {
    const obj = JSON.parse(data);
    switch (obj.command) {
        case "circle":
            gr.circle(obj.params.x, obj.params.y, obj.params.radius);
            break;
        case "textbox":
            gr.textbox(obj.params.x, obj.params.y, obj.params.text);
            break;
        case "item-REST":
            getAsset('REST.svg', function (data) {
                let newItem = gr.addItem(data, obj.params.id);
                newItem.addEventListener('click', item_onClick);
            });
            break;
        case "item_Update":
            gr.updateItem(obj.params.ID, obj.params);
            break;
        case "list":
            gr.removeAll();
            getAsset('REST.svg', function (data) {
                gr.ProjectTitle = obj.params.Title;
                gr.ProjectDescription = obj.params.Description;
                gr.ProjectPort = obj.params.Port;
                let list = obj.params.Items;
                for (let item in list) {
                    let newItem = gr.addItem(data, list[item].ID);
                    newItem.addEventListener('click', item_onClick);
                    console.log("adding item " + list[item].ID);
                    gr.updateItem(list[item].ID, list[item]);
                }
                gr.updateGraph();
            });
            break;
    }
}


window.addEventListener("load", () => {
    gr = new SVG("graph");
    gr.removeAll();
});

function item_onClick(obj, e) {
    let idObj = obj.currentTarget.getAttribute('id').replace('item-', '');
    window.chrome.webview.postMessage({ msg: "item_Clicked", id: idObj });
}


//**************************************** */
// Helpers

function getAsset(asset, callback) {
    let http = new XMLHttpRequest();
    let url = 'https://appassets/' + asset;
    http.open('GET', url, true);
    http.onreadystatechange = function () {
        if (http.readyState == 4 && http.status == 200) {
            callback(http.responseText);
        }
    }
    http.send();
}



