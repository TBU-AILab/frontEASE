window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = fileName;
    anchor.click();
    URL.revokeObjectURL(url);
};

window.monacoEditors = {};

window.monacoCreate = function (elementId, value, dotNetRef) {
    require(['vs/editor/editor.main'], function () {
        const container = document.getElementById(elementId);
        if (!container) return;

        const isDark = document.documentElement.getAttribute('data-bs-theme') === 'dark'
            || document.body.classList.contains('dark')
            || window.matchMedia('(prefers-color-scheme: dark)').matches;

        const editor = monaco.editor.create(container, {
            value: value || '',
            language: 'python',
            theme: 'vs-dark',
            minimap: { enabled: false },
            automaticLayout: true,
            fontSize: 13,
            lineNumbers: 'on',
            scrollBeyondLastLine: false,
            wordWrap: 'off',
            tabSize: 4,
            insertSpaces: true,
            renderLineHighlight: 'line',
            scrollbar: { verticalScrollbarSize: 8, horizontalScrollbarSize: 8 }
        });

        editor.onDidChangeModelContent(function () {
            dotNetRef.invokeMethodAsync('OnContentChanged', editor.getValue());
        });

        window.monacoEditors[elementId] = editor;
    });
};

window.monacoSetValue = function (elementId, value) {
    const editor = window.monacoEditors[elementId];
    if (editor) {
        editor.setValue(value || '');
    }
};

window.monacoGetValue = function (elementId) {
    const editor = window.monacoEditors[elementId];
    return editor ? editor.getValue() : '';
};

window.monacoDispose = function (elementId) {
    const editor = window.monacoEditors[elementId];
    if (editor) {
        editor.dispose();
        delete window.monacoEditors[elementId];
    }
};