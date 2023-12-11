let editor;

ClassicEditor
    .create(document.querySelector('#editor'))
    .then(newEditor => {
        editor = newEditor;
        let plainTextData = document.getElementById("my-data").value;
        editor.setData(plainTextData);
    })
    .catch(error => {
        console.error(error);
    });