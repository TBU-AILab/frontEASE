window.codeEditor = {
    _instances: {},

    create: function (elementId, language, readOnly, value) {
        const container = document.getElementById(elementId);
        if (!container || this._instances[elementId]) { return; }

        const editor = ace.edit(container, {
            mode: "ace/mode/" + (language || "json"),
            theme: "ace/theme/one_dark",
            value: value || "",
            readOnly: readOnly || false,
            fontSize: 14,
            fontFamily: "'Consolas', 'Courier New', monospace",
            showPrintMargin: false,
            tabSize: 2,
            useSoftTabs: true,
            wrap: false,
            minLines: 20,
            maxLines: 40,
            showGutter: true,
            highlightActiveLine: true,
            enableBasicAutocompletion: false
        });

        this._instances[elementId] = editor;
    },

    getValue: function (elementId) {
        const editor = this._instances[elementId];
        return editor ? editor.getValue() : "";
    },

    setValue: function (elementId, value) {
        const editor = this._instances[elementId];
        if (editor) {
            editor.setValue(value || "", -1);
            editor.clearSelection();
        }
    },

    setReadOnly: function (elementId, readOnly) {
        const editor = this._instances[elementId];
        if (editor) {
            editor.setReadOnly(readOnly);
        }
    },

    setLanguage: function (elementId, language) {
        const editor = this._instances[elementId];
        if (editor) {
            editor.session.setMode("ace/mode/" + language);
        }
    },

    resize: function (elementId) {
        const editor = this._instances[elementId];
        if (editor) {
            editor.resize();
        }
    },

    dispose: function (elementId) {
        const editor = this._instances[elementId];
        if (editor) {
            editor.destroy();
            delete this._instances[elementId];
        }
    }
};
