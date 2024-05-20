"use strict";
// @ts-check

/**
 * File:            view.js
 * Author:          Fred de Laszlo
 * Version:         1.0.0
 * Date:            10-05-2024
 * Copyright:       (C) Fred de Laszlo, 2023
 * License:         MIT
 * 
 * Controller class, comminicates with c# main program
 */


let gr = null;  // The graphic object

/** 
 * Listens for events from c# main program
 * and dispatches it
 */
window.chrome.webview.addEventListener('message', event => {
    WriteDataFromCsharp(event.data);
});

/**
 * Dispatches events from main c# program
 * @param {string} data -  The json encoded string 
 */
function WriteDataFromCsharp(data) {
    const obj = JSON.parse(data);
    switch (obj.command) {
        case "item-REST":   
            // Add a new node
            getAsset('REST.svg', function (data) {
                let newItem = gr.addItem(data, obj.params.id);
                newItem.addEventListener('click', item_onClick);
            });
            break;
        case "item_Update":
            // Update a node
            gr.updateItem(obj.params.ID, obj.params);
            break;
        case "list":
            // New list of nodes
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

/**
 * Create a new svg instance when page has loaded
 */
window.addEventListener("load", () => {
    gr = new SVG("graph");
    gr.removeAll();
});

/**
 * User clicked on a node,
 * send details to main c# program
 * @param {object} obj - The node object that was clicked
 * @param {any} e - The object event
 */
function item_onClick(obj, e) {
    // Get the node id...
    let idObj = obj.currentTarget.getAttribute('id').replace('item-', '');
    // ... and send it to the main c# program
    window.chrome.webview.postMessage({ msg: "item_Clicked", id: idObj });
}


//**************************************** */
// Helpers

/**
 * Load a file asset
 * @param {string} asset - The file to load
 * @param {any} callback - The callback function to call when assset has loaded
 */
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



