const tblBlog = "blogs";
let blogId = '';

Run();
function Run() {
    getBlogList();
    //readBlog();
    //createBlog();
    //updateBlog('14d93239-5662-49fb-9a57-28656584069f', 'pont', 'pont', 'pont');
    //deleteBlog("efe991d2-bdda-422e-a5cf-0fe6afa5a3e6");
}
function readBlog() {
    let lst = getBlog();
    console.log(lst);
}


function createBlog(title, author, content) {
    let lst = getBlog();
    const blog = {
        Id: uuidv4(),
        Title: title,
        Author: author,
        Content: content
    }
    lst.push(blog);
    setBlog(lst);
    successMessage("Save Successful.")
    clear();
}
function editBlog(id) {
    let lst = getBlog();
    var blog = lst.filter(x => x.Id == id);

    if (blog.length == 0) {
        console.log("No Data Found");
        erroerMessage("No Data Found");
        return;
    }
    const blogData = blog[0];
    blogId = blogData.Id;
    $('#txtTitle').val(blogData.Title);
    $('#txtAuthor').val(blogData.Author);
    $('#txtContent').val(blogData.Content);
    $('#txtTitle').focus();
}
function updateBlog(id, title, author, content) {
    let lst = getBlog();
    var blog = lst.filter(x => x.Id == id);

    if (blog.length == 0) {
        console.log("No Data Found");
        return;
    }
    const getblog = blog[0];
    getblog.Title = title;
    getblog.Author = author;
    getblog.Content = content;

    const index = lst.findIndex(x => x.Id == id);
    lst[index] = getblog;
    setBlog(lst);
    successMessage("Updated Successful!.")
    clear();
}
function deleteBlog(id) {
    const result= confirm("Are you sure want to delete?");
    if(!result) return;
    let lst = getBlog();
    var blog = lst.filter(x => x.Id == id);

    if (blog.length == 0) {
        console.log("No Data Found");
        return;
    }

    lst = lst.filter(x => x.Id != id);
    setBlog(lst);
    successMessage("Deleted Successful!");
    getBlogList();
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

function setBlog(lst) {
    const jsonStr = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonStr);
}

function getBlog() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);

    let lst = [];
    if (blogs != null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}


$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();
    if (blogId == '') {
        createBlog(title, author, content);
    }
    else {
        updateBlog(blogId, title, author, content);
        blogId = '';
    }

    getBlogList();
})

function successMessage(message) {
    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success"
    });
}
function erroerMessage(message) {
    Swal.fire({
        title: "Fail!",
        text: message,
        icon: "error"
    });
}

function clear() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function getBlogList() {
    $('#tbody').html('');
    let lst = getBlog();
    let count = 0;
    let rowHtmls = '';
    lst.forEach(blog => {
        const rowHtml = `
        <tr>
                <td>
                <button type="button" class="btn btn-warning" onclick=editBlog('${blog.Id}')>Edit</button>
                <button type="button" class="btn btn-danger" onclick=deleteBlog('${blog.Id}')>Delete</button>
                </td>
                <th scope="row">${++count}</th>
                <td>${blog.Title}</td>
                <td>${blog.Author}</td>
                <td>${blog.Content}</td>
              </tr>   
        `;
        rowHtmls += rowHtml;

    });

    $('#tbody').html(rowHtmls);
}
