function customConfirm(text) {
    return new Promise(resolve => {
        Swal.fire({
            title: "Are you sure?",
            text: text,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Delete",
            confirmButtonColor:"red"
        }).then((result) => {
            resolve(result.isConfirmed)
        });
    })
    
}