
function DeleteProf(name, userId) {
    Swal.fire({
        title: ` آیا از حذف پروفایل ${name} مطمعن هستید؟`,
        icon: 'question',
        showCloseButton: true,
        showCancelButton: true,
        confirmButtonText: 'بله ',
        cancelButtonText: 'خیر',
    })
        .then(res => {
            if (res.isConfirmed) {
                fetch(`/Admin/User/UserDetailDelete/${userId}`, { method: 'GET' })
            }
            else {
                console.log('no');
            }
        })
}