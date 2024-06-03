"use strict";
// @ts-check

/**
 * File:            svg.js
 * Author:          Fred de Laszlo
 * Version:         1.0.0
 * Date:            10-05-2024
 * Copyright:       (C) Fred de Laszlo, 2023
 * License:         MIT
 * 
 * View class to display nodes
 */


class SVG {
    #svgns = "http://www.w3.org/2000/svg";
    #svg = null;

    constructor(id, elem = null) {
        if (elem !== null) {
            // svg is emebedded inside object element
            let svgObject = document.querySelector(elem);
            this.#svg = svgObject.contentDocument.getElementById(id); 
        } else {
            this.#svg = document.getElementById(id);
        }
    }

    /**
     * Draw a line from (x1, y1) to (x2, y2). 
     * @param {Number} x1   - x1 coordinate
     * @param {Number} y1   - y1 coordinate
     * @param {Number} x2   - x2 coordinate
     * @param {Number} y2   - y2 coordinate
     * @returns {void} 
     */
    line(x1, y1, x2, y2) {
        let s = {};
        s.x1 = x1.toString();
        s.x2 = x2.toString();
        s.y1 = y1.toString();
        s.y2 = y2.toString();
        let newLine = document.createElementNS(this.#svgns, 'line');
        this.setAttributes(newLine, s);
        this.#svg.appendChild(newLine);
    }


    ////////////////////////////////////////////////////////////////////////////////////

    #idPrefix = 'item-';

    ProjectTitle = '';
    ProjectDescription = '';
    ProjectPort = 8080;

    /**
     * Create a new node
     * @param {object} data - The HTML of the new node 
     * @param {integer} id - The id of the new node
     * @returns {object} The newly created object
     */
    addItem(data, id) {
        let idText = this.#getItemID(id);
        data = data.replace("id=\"\"", "id=\"" + idText + "\"")
        this.#svg.insertAdjacentHTML('beforeend', data);
        this.alignItemContent(id);
        return this.getItem(id);
    }

    /**
     * Get the node object associated with an ID
     * @param {integer} id - The node ID
     * @returns {object} The node object or null if not found
     */
    getItem(id) {
        let idText = this.#getItemID(id);
        return this.#svg.getElementById(idText);
    }

    /**
     * Remove all nodes.
     */
    removeAll() {
        while (this.#svg.firstChild !== null) {
            this.#svg.removeChild(this.#svg.firstChild);
        }
    }



    /**
     * Update visual aspects of a an item (node). All text is positioned
     * inside the node and the bounding rectangles sized to encompass 
     * the text. CSS rules in view.css are used to update the node.
     * @param {integer} id  - The node to update
     * @param {array} params - A list of node parameters (node contents)
     */
    updateItem(id, params) {

        // Functions used by updateItem

        /**
         * Split a string into an array of word-wrapped lines
         * @param {string} str - The string to word wrap
         * @param {integer} max - Maximum number characters per line
         * @returns An array of word-wrapped lines
         */
        function wordWrap(str, max) {
            let br = '\n';
            let regex = new RegExp(`(?![^\\n]{1,${max}}$)([^\\n]{1,${max}})\\s`, 'g');
            let newStr = str.replace(regex, `$1${br}`);
            return newStr.split(br);
        }

        /**
         * Turn a list of data expected and types (string/number) into HTML tspans.
         * @param {array} list - List of keys and key types that a method (GET/POST/etc.) expects
         * @param {string} arrow - Direction of data (data expected/returned) as shown by HTML character code for arrow
         * @returns An HTML string with list split into tspans.
         */
        function expects(list, arrow) {
            let HTML = '';
            for (let key in list) {
                if (typeof list[key] !== 'function') {
                    let value = list[key];
                    HTML += `<tspan x="0" dy="0" class="expects">${arrow}(${value})${key}</tspan>`;
                }
            }
            return HTML;
        }

        // Get the item (node)
        let item = this.getItem(id);
        if (item) {
            // Set the title of the node (usually a noun)
            let title = item.getElementsByClassName('title')[0];
            let noun = params['Noun'] === 'id' ? '{id}' : params['Noun'];
            title.innerHTML = noun; 

            // If the title is 'id', add id class to the title (view.css)
            if (noun === '{id}') {
                title.classList.add('id');
            }

            
            // The title (noun)
            {
                // Word-wrap description into lines, get max chars from view.css (.description max-width) 
                let maxCharsPerLine = parseFloat(this.#getCssRuleStyle('https://appassets/view.css', '.description', 'max-width'));
                let lines = wordWrap(params['Description'], maxCharsPerLine);

                // for each line, create a tspan
                let tspan = '';
                for (let i = 0; i < lines.length; i++) {
                    tspan += '<tspan x="0" dy="0">' + lines[i] + '</tspan>';
                }
                item.getElementsByClassName('description')[0].innerHTML = tspan;

                // adjust dy in description tspans to allow room based on font size
                let descrip = item.getElementsByClassName('description')[0];
                let tspans = descrip.getElementsByTagName('tspan');
                let dy = 0;
                if (tspans.length > 0) {
                    let bbox = tspans[0].getBBox(); // get bounding box of font
                    dy = bbox.height;
                    for (let i = 1; i < tspans.length; i++) {
                        tspans[i].setAttribute('dy', dy);
                    }
                }
            }

            // Fill methods info
            {
                //Get the info element and style 
                let info = item.getElementsByClassName('info')[0];
                let style = this.#getStyle(info);

                // For each of GET/POST/PATCH/DELETE, 
                // add method anmd whether authorisation is needed
                let HTML = '';
                let useGET = params['UseGET'] === false ? '' : (params['UseGETAuthorisation'] === true ? '🔒GET' : 'GET');
                let usePOST = params['UsePOST'] === false ? '' : (params['UsePOSTAuthorisation'] === true ? '🔒POST' : 'POST');
                let usePATCH = params['UsePATCH'] === false ? '' : (params['UsePATCHAuthorisation'] === true ? '🔒PATCH' : 'PATCH');
                let useDELETE = params['UseDELETE'] === false ? '' : (params['UseDELETEAuthorisation'] === true ? '🔒DELETE' : 'DELETE');

                // For each method, add the data expected and data returned
                HTML += useGET.length > 0 ? `<tspan x="0" dy="0">${useGET}</tspan>` + expects(params['ExpectsGET'], '&rArr;') + expects(params['ReturnsGET'], '&lArr;') : '';
                HTML += usePOST.length > 0 ? `<tspan x="0" dy="0">${usePOST}</tspan>` + expects(params['ExpectsPOST'], '&rArr;') + expects(params['ReturnsPOST'], '&lArr;') : '';
                HTML += usePATCH.length > 0 ? `<tspan x="0" dy="0">${usePATCH}</tspan>` + expects(params['ExpectsPATCH'], '&rArr;') + expects(params['ReturnsPATCH'], '&lArr;') : '';
                HTML += useDELETE.length > 0 ? `<tspan x="0" dy="0">${useDELETE}</tspan>` + expects(params['ExpectsDELETE'], '&rArr;') + expects(params['ReturnsDELETE'], '&lArr;') : '';

                info.innerHTML = HTML;

                // adjust dy and x value of tspans 
                let tspans = info.getElementsByTagName('tspan');
                let dy = 0;
                if (tspans.length > 0) {
                    let bbox = tspans[0].getBBox();
                    dy = bbox.height;
                    for (let i = 1; i < tspans.length; i++) {
                        tspans[i].setAttribute('dy', dy);
                    }
                }

                for (let i = 0; i < tspans.length; i++) {
                    tspans[i].setAttribute('x', style.paddingLeft);
                }
            }

            // add the rest of the data
            for (let data in params) {
                switch (data) {
                    case 'Noun':
                    case 'Description':
                    case 'UseGET':
                    case 'UsePOST':
                    case 'UsePATCH':
                    case 'UseDELETE':
                        break;
                    default:
                        item[data] = params[data];
                        break;
                }
            }
        }
    }

    /**
     * Recursively finds attached nodes along a single path
     * @param {object} node - The node to find all attached nodes in a single path
     * @param {array} allNodes - All the nodes
     * @returns
     */
    #getConnectedNodes(node, allNodes) {
        let map = [[{ id: node.ID }]]; // Current path
        let altNodes = [];  // Nodes that have more than one route from that node
        
        // Collect nodes from initial node
        let connectedNodes = allNodes.filter((n) => n.PreviousNodeID === node.ID);
        while (connectedNodes.length > 0) {
            // Save the node on current path
            map[0].push({ id: connectedNodes[0].ID });
            // Save extra nodes for recursive node search
            for (let i = 1; i < connectedNodes.length; i++) {
                altNodes.unshift(connectedNodes[i]); 
            }
            // Next node on path
            connectedNodes = allNodes.filter((n) => n.PreviousNodeID === connectedNodes[0].ID);
        }
        // Add any sub nodes (recursion)
        for (let i = 0; i < altNodes.length; i++) {
            let newMap = this.#getConnectedNodes(altNodes[i], allNodes);
            for (let i = 0; i < newMap.length; i++) {
                map.push(newMap[i]);
            }
        }
        return map;
    }

    /**
     * 
     * @returns An array of rows and columns with relative (visual) positions of all nodes. 
     */
    #makeMap() {
        let map = [];
        let row = 0;
        // Find all the nodes in SVG
        let nodes = Array.from(this.#svg.getElementsByClassName('item-REST'));
        // get the root node (should only be one)
        let rootNode = nodes.find((node) => node.PreviousNodeID < 0);
        // build connected nodes from root node
        let newMap = this.#getConnectedNodes(rootNode, nodes);
        // Rearrange (sensibly) the newMap to map such that each map row is a path.
        // push(newMap) would simply add the entire newMap array into a single cell without this.
        for (let i = 0; i < newMap.length; i++) {
            map.push(newMap[i]);
        }
            
        return map;
    }

    
    updateGraph() {

        // Functions used in updateGraph
        /**
         * Update text on svg canvas
         * @param {object} objThis - The 'this' object
         * @param {object} svg - The SVG object
         * @param {string} detail - The project detail to update 
         * @param {string} text - The text to insert
         * @param {number} startY - Start Y position
         * @returns {number} - startY + height of detail
         */
        function updateProjectDetail(objThis, svg, detail, text, startY) {
            // Find (creating if necessary) element
            let elem = svg.getElementById(detail);
            if (!elem) {
                svg.insertAdjacentHTML('beforeend', `<text id='${detail}'></text>`);
                elem = svg.getElementById(detail);
            }
            // Adjust element
            elem.innerHTML = text;
            let style = objThis.#getStyle(elem);
            objThis.setAttributes(elem, { x: style.paddingLeft, y: startY + style.paddingTop, 'dominant-baseline': 'text-before-edge' });
            let bbox = elem.getBoundingClientRect();
            return bbox.height > 0 ? bbox.bottom : startY;
        }

        // Get all nodes and convert too a usable array
        let nodes = this.#svg.getElementsByClassName('item-REST');
        let arNodes = Array.from(nodes);
        let style = this.#getStyle(nodes[0]);

        // Set position of project title, description and listening port
        let startY = updateProjectDetail(this, this.#svg, 'project-title', this.ProjectTitle, 0);
        startY = updateProjectDetail(this, this.#svg, 'project-description', this.ProjectDescription, startY);
        startY = updateProjectDetail(this, this.#svg, 'project-port', `Listening on port: ${this.ProjectPort.toString()}`, startY);
        startY += style.paddingTop;

        // create a map of nodes
        let map = this.#makeMap();

        // align contents of all nodes
        for (let i = 0; i < nodes.length; i++) {
            this.alignItemContent(nodes[i].id);
        }

        let maxRight = 0;
        let maxBottom = 0;

        // use map to determine each node placement
        // and draw lines between connected nodes
        for (let row = 0; row < map.length; row++) {
            let maxHeight = 0
            for (let col = 0; col < map[row].length; col++) {
                let node = this.getItem(map[row][col].id);
                let bbox = node.getBBox();
                let pos = { x: 0, y: 0, width: bbox.width, height: bbox.height };
                if (maxHeight < pos.height) { maxHeight = pos.height };
                // calculate x
                if (node.PreviousNodeID < 0) {
                    pos.x = style.paddingLeft;
                    gr.line(10, startY + 10, pos.x, startY + 10);
                } else {
                    // get previous nodes details
                    let prevNodePos = arNodes.find((n) => n.ID === node.PreviousNodeID).getBoundingClientRect();
                    pos.x = prevNodePos.x + prevNodePos.width + style.paddingLeft + style.paddingRight;
                    // draw a line
                    if (prevNodePos.y == startY) {
                        // straight line
                        gr.line(prevNodePos.x + prevNodePos.width, prevNodePos.y + 10, pos.x, startY + 10);
                    } else {
                        // Not on same row...
                        gr.line(prevNodePos.x + prevNodePos.width + style.paddingRight,
                            prevNodePos.y + 10,
                            prevNodePos.x + prevNodePos.width + style.paddingRight,
                            startY + 10
                        );
                        gr.line(prevNodePos.x + prevNodePos.width + style.paddingRight,
                            startY + 10, pos.x, startY + 10
                        );
                    }
                }
                maxRight = maxRight < (pos.x + pos.width + style.paddingRight) ? (pos.x + pos.width + style.paddingRight) : maxRight;
                pos.y = startY;
                map[row][col].pos = pos;
                // Calculate y 
                let id = this.#getItemID(map[row][col].id);
                document.getElementById(id).setAttribute('transform', 'translate(' + pos.x + ', ' + pos.y + ')');

            }
            startY += maxHeight + style.paddingTop + style.paddingBottom;
        }
        maxBottom = startY;
        // Set width and height of svg component
        document.getElementById("graph").style.width = "" + (maxRight + 40) + "px";
        document.getElementById("graph").style.height = "" + (maxBottom + 20) + "px";
    }

    /**
     * Get the raw style from css file
     * @param {string} asset - The filename of the CSS file (e.g. view.css)
     * @param {string} selector - The selector within the CSS file (class, id, etc.)
     * @param {string} style - The style name  within the selector (e.g. width)
     * @returns {string} The style rule value or empty string if not found
     */
    #getCssRuleStyle(asset, selector, style) {
        for (let sheet = 0; sheet < document.styleSheets.length; sheet++) {
            if (document.styleSheets[sheet].href === asset) {
                let cssRules = document.styleSheets[sheet].cssRules;
                for (let rule = 0; rule < cssRules.length; rule++) {
                    if (cssRules[rule].selectorText === selector) {
                        return cssRules[rule].style[style];
                    }
                }
            }
        }
        return '';
    }

    /**
     * Create a custom styles list of an element
     * @param {object} elem - The element to create a styles list for
     * @returns List of styles (disctionary)  as an array of key value pairs
     */
    #getStyle(elem) {
        let style = window.getComputedStyle(elem);
        let ret = {
            x: isNaN(parseInt(style.x)) ? 0 : parseInt(style.x),
            y: isNaN(parseInt(style.y)) ? 0 : parseInt(style.y),
            fontSize: isNaN(parseInt(style.fontSize)) ? 0 : parseInt(style.fontSize),
            textAnchor: style.textAnchor,
            paddingLeft: isNaN(parseInt(style.paddingLeft)) ? 0 : parseInt(style.paddingLeft),
            paddingTop: isNaN(parseInt(style.paddingTop)) ? 0 : parseInt(style.paddingTop),
            paddingRight: isNaN(parseInt(style.paddingRight)) ? 0 : parseInt(style.paddingRight),
            paddingBottom: isNaN(parseInt(style.paddingBottom)) ? 0 : parseInt(style.paddingBottom),
            left: parseFloat(style.left),
            top: parseFloat(style.top),
            width: parseFloat(style.width),
            height: parseFloat(style.height),
            maxCharsPerLine: parseFloat(this.#getCssRuleStyle('https://appassets/view.css', '.description', 'max-width'))
         };

        return ret;
    }

    /**
     * Aligns text left centre or right of a text element to a bounding rectangle
     * @param {object} style - An array holding style key value pairs
     * @param {object} styleRect - The bounding rectangle to position the text
     * @returns Returns the adjusted style information with new x and y coordinates
     */
    #alignText(style, styleRect) {
        // Align text with padding
        // ensure dominant-baseline is text-before-edge so calculations are correct
        style["dominant-baseline"] = 'text-before-edge';
        style.y = styleRect.y + style.paddingTop;

        // calculate x position
        switch (style.textAnchor) {
            case 'middle':
                style.x = styleRect.x + (styleRect.width / 2);
                break;
            case 'end':
                style.x = styleRect.x + styleRect.width - style.paddingRight;
                break;
            default:
                // default is 'start' aka left-aligned
                style.x = styleRect.x + style.paddingLeft;
                break;
        }
        return style;
    }

    /**
     * Align all contents of a node
     * @param {integer} id - The node ID
     */
    alignItemContent(id) {
        let item = null;
        if (typeof (id) == "string") {
            item = document.getElementById(id);
        } else {
            item = this.getItem(id);
        }

        //Compute title position, allowing for css padding
        let title = item.querySelector('.title');
        let desc = item.querySelector('.description');
        let info = item.querySelector('.info');
        let titleRect = item.querySelector('.titleRect');
        let descRect = item.querySelector('.descriptionRect');
        let infoRect = item.querySelector('.infoRect');
        let clickRect = item.querySelector('.clickRect');

        let styleTitle = this.#getStyle(title);
        let styleTitleRect = this.#getStyle(titleRect);
        let styleDesc = this.#getStyle(desc);
        let styleDescRect = this.#getStyle(descRect);
        let styleInfo = this.#getStyle(info);
        let styleInfoRect = this.#getStyle(infoRect);
        let styleClickRect = this.#getStyle(clickRect);

        // get the maximum width to size the rect to text
        let titleBounds = title.getBoundingClientRect();
        let titleWidth = styleTitle.paddingLeft + titleBounds.width + styleTitle.paddingRight;
        let descBounds = desc.getBoundingClientRect();
        let descWidth = styleDesc.paddingLeft + descBounds.width + styleDesc.paddingRight;
        let newWidth = titleWidth > descWidth ? titleWidth : descWidth;
        let infoBounds = info.getBoundingClientRect();
        let infoWidth = styleInfo.paddingLeft + infoBounds.width + styleInfo.paddingRight;
        newWidth = infoWidth > newWidth ? infoWidth : newWidth;


        styleTitleRect.width = newWidth;
        styleDescRect.width = newWidth;
        styleInfoRect.width = newWidth;
        styleClickRect.width = newWidth;

        //calculate text height (title uses one line, so use font size rather than computed size)
        styleTitleRect.height = styleTitle.paddingTop + styleTitle.fontSize + styleTitle.paddingBottom;
        styleDescRect.height = descBounds.height === 0 ? 0 : styleDesc.paddingTop + descBounds.height + styleDesc.paddingBottom;
        styleDescRect.y = styleTitleRect.y + styleTitleRect.height;
        styleInfoRect.height = infoBounds.height === 0 ? 0 : styleInfo.paddingTop + infoBounds.height + styleInfo.paddingBottom;
        styleInfoRect.y = styleDescRect.y + styleDescRect.height;
        styleClickRect.height = styleTitleRect.height + styleDescRect.height + styleInfoRect.height;

        // Set text positions
        styleTitle = this.#alignText(styleTitle, styleTitleRect);
        styleDesc = this.#alignText(styleDesc, styleDescRect);
        styleInfo = this.#alignText(styleInfo, styleInfoRect);

        this.setAttributes(title, styleTitle);
        this.setAttributes(titleRect, styleTitleRect);
        this.setAttributes(desc, styleDesc);
        this.setAttributes(descRect, styleDescRect);
        this.setAttributes(info, styleInfo);
        this.setAttributes(infoRect, styleInfoRect);
        this.setAttributes(clickRect, styleClickRect);

        // Finally, apply x value to all tspan(s) in description
        let tspans = desc.getElementsByTagName('tspan');
        for (let i = 0; i < tspans.length; i++) {
            tspans[i].setAttribute('x', styleDesc.x);
        }
        tspans = info.getElementsByTagName('tspan');
        for (let i = 0; i < tspans.length; i++) {
            tspans[i].setAttribute('x', styleInfo.paddingLeft);
        }

    }

    /**
     * Returns id from integer
     * @param {any} id
     * @returns {string}
     */
    #getItemID(id) {
        return this.#idPrefix + id;
    }

     
    /**
     *  Sets attributes of a given svg object
     *  @param {Object} obj - SVG object
     *  @param {Object} attribs - List of attributes
     *  @returns {void}
     */
    setAttributes(obj, attribs) {
        for (var key in attribs) {
            obj.setAttributeNS(null, key, attribs[key]);
        }
    }

    /**
     * Convert from window x, y to SVG coordinates 
     * @param {type} clientX
     * @param {type} clientY
     * @returns {SVG.getXY.svgPt}
     */
    getXY(clientX, clientY) {
        let svgPt = {"x": 0, "y": 0};
        if (this.#svg !== null) {
            let pt = this.#svg.createSVGPoint();
            pt.x = clientX;
            pt.y = clientY;
            svgPt = pt.matrixTransform(this.#svg.getScreenCTM().inverse());
        }
        return svgPt;
    }

 }


