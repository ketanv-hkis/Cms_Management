function DeleteTask(Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Task/DeleteTask',
                type: 'POST',
                data: { Id: Id },
                success: function (data) {
                    if (data == "success") {
                        Swal.fire(
                            'Deleted!',
                            'Your employee has been deleted.',
                            'success'
                        );
                        setTimeout(function () {
                            window.location.reload();
                        }, 1000)
                        
                    }
                }
            })
        }
    });
}