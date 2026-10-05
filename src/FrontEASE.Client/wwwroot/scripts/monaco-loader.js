/*
 * Lazy monaco-editor loader.
 *
 * monaco's loader.js registers a global AMD `define`. When a UMD bundle (e.g. easymde,
 * lazy-loaded by Blazorise.Markdown) executes afterwards, its anonymous `define()` call
 * is intercepted by monaco's loader and rejected with:
 *   "Can only have one anonymous define call per script file".
 *
 * To avoid the conflict, monaco is loaded lazily on first use, and the AMD globals are
 * temporarily removed while other (non-AMD) scripts execute.
 */
(function () {
    const MONACO_BASE = 'https://cdn.jsdelivr.net/npm/monaco-editor@0.52.0/min/vs';

    let loaderPromise = null;

    window.withoutAmd = function (action) {
        const hadDefine = Object.prototype.hasOwnProperty.call(window, 'define');
        const hadRequire = Object.prototype.hasOwnProperty.call(window, 'require');
        const savedDefine = window.define;
        const savedRequire = window.require;

        delete window.define;
        delete window.require;

        try {
            return action();
        }
        finally {
            if (hadDefine) window.define = savedDefine;
            if (hadRequire) window.require = savedRequire;
        }
    };

    function loadScript(src) {
        return window.withoutAmd(() => new Promise((resolve, reject) => {
            const script = document.createElement('script');
            script.src = src;
            script.onload = () => resolve();
            script.onerror = () => reject(new Error('Failed to load script: ' + src));
            document.head.appendChild(script);
        }));
    }

    window.monacoLoader = {
        load: function () {
            if (!loaderPromise) {
                loaderPromise = loadScript(MONACO_BASE + '/loader.js').then(() => {
                    window.require.config({ paths: { 'vs': MONACO_BASE } });
                    return new Promise((resolve) => {
                        window.require(['vs/editor/editor.main'], () => resolve());
                    });
                });
            }

            return loaderPromise;
        }
    };
})();
