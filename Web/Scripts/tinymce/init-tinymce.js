tinymce.init({
    selector: "textarea.tinymce",
    plugins: 'advlist autolink link image lists charmap print preview',
    formats: {
        alignleft: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'left' },
        aligncenter: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'center' },
        alignright: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'right' },
        alignjustify: { selector: 'p,h1,h2,h3,h4,h5,h6,td,th,div,ul,ol,li,table,img', classes: 'full' },
        bold: { inline: 'span', 'classes': 'bold' },
        italic: { inline: 'span', 'classes': 'italic' },
        underline: { inline: 'span', 'classes': 'underline', exact: true },
        strikethrough: { inline: 'del' },
        forecolor: { inline: 'span', classes: 'forecolor', styles: { color: '%value' } },
        hilitecolor: { inline: 'span', classes: 'hilitecolor', styles: { backgroundColor: '%value' } },
        custom_format: { block: 'h1', attributes: { title: 'Header' }, styles: { color: 'red' } }
    },
    templates: [
        { title: 'Report Type 1', description: 'Type 1 doctor\'s report', url: '/Scripts/tinymce/tinytemplates/docreport1.html' }
    ],
    theme: 'modern',
    //width: 600,
    height: 300,
    plugins: [
        'advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker',
        'searchreplace wordcount visualblocks visualchars code insertdatetime media nonbreaking',
        'save table contextmenu directionality emoticons template paste textcolor'
    ],
    content_css: [
        '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
        '//www.tinymce.com/css/codepen.min.css'],
    toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor emoticons',
    // enable title field in the Image dialog
    image_title: true,
    // enable automatic uploads of images represented by blob or data URIs
    automatic_uploads: true,
    // URL of our upload handler (for more details check: https://www.tinymce.com/docs/configure/file-image-upload/#images_upload_url)
    // images_upload_url: 'postAcceptor.php',
    // here we add custom filepicker only to Image dialog
    file_picker_types: 'image',
    // and here's our custom image picker
    file_picker_callback: function (cb, value, meta) {
        var input = document.createElement('input');
        input.setAttribute('type', 'file');
        input.setAttribute('accept', 'image/*');

        // Note: In modern browsers input[type="file"] is functional without 
        // even adding it to the DOM, but that might not be the case in some older
        // or quirky browsers like IE, so you might want to add it to the DOM
        // just in case, and visually hide it. And do not forget do remove it
        // once you do not need it anymore.

        input.onchange = function () {
            var file = this.files[0];

            var reader = new FileReader();
            reader.onload = function () {
                // Note: Now we need to register the blob in TinyMCEs image blob
                // registry. In the next release this part hopefully won't be
                // necessary, as we are looking to handle it internally.
                var id = 'blobid' + (new Date()).getTime();
                var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                var base64 = reader.result.split(',')[1];
                var blobInfo = blobCache.create(id, file, base64);
                blobCache.add(blobInfo);

                // call the callback and populate the Title field with the file name
                cb(blobInfo.blobUri(), { title: file.name });
            };
            reader.readAsDataURL(file);
        };

        input.click();
    },

    setup: function (ed) {
        //!---Call the save function here

        //ed.on('keyup', function (e) {
        //    tinyMceChange(ed);
        //});
        ed.on('focusout', function (e) {
            tinyMceChange(ed);
        });
    }
});