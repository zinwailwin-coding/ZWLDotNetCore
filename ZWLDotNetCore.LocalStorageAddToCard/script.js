let tbl_cart = 'cart';
let totalPrice = 0;
shoppingCart();

function addtoCard(item) {
    let lst = localStorage.getItem(tbl_cart);
    let cartList = lst ? JSON.parse(lst) : [];

    const existingItem = cartList.find(cartItem => cartItem.id === item.id);

    if (existingItem) {
        existingItem.quantity += 1;
    } else {
        cartList.push(item);
    }
    localStorage.setItem(tbl_cart, JSON.stringify(cartList));
    shoppingCart();
}

function getCartList() {
    let lst = localStorage.getItem(tbl_cart);
    return lst ? JSON.parse(lst) : [];
}

function shoppingCart() {
    $('#cartItems').html('');
    totalPrice = 0;
    let htmlRows = '';
    let cartList = getCartList();

    cartList.forEach(cart => {
        if (cart.quantity > 0) {
            totalPrice += cart.quantity * cart.price;
            const htmlRow = `
            <tr>
                <td>${cart.name}</td>
                <td>
                    <div class="quantity-container">
                        <button class="quantity-button" onclick="decrementQuantity('${cart.id}')">-</button>
                        <span id="quantity-${cart.id}">${cart.quantity}</span>
                        <button class="quantity-button" onclick="incrementQuantity('${cart.id}')">+</button>
                    </div>
                </td>
                <td>${cart.price.toFixed(2)} MMK</td>
            </tr>
            `;
            htmlRows += htmlRow;
        } else {
            cartList = cartList.filter(x => x.id !== cart.id);
        }
    });

    localStorage.setItem(tbl_cart, JSON.stringify(cartList));
    $('#cartItems').html(htmlRows);
    $('#cartFooter').text(totalPrice.toFixed(2) + ' MMK');
}

function updateCartItemQuantity(id, delta) {
    let cartList = getCartList();
    const cartItem = cartList.find(item => item.id === id);

    if (cartItem) {
        cartItem.quantity += delta;
        if (cartItem.quantity < 0) {
            cartItem.quantity = 0;
        }

        localStorage.setItem(tbl_cart, JSON.stringify(cartList));
        shoppingCart();
    }
}

function incrementQuantity(id) {
    updateCartItemQuantity(id, 1);
}

function decrementQuantity(id) {
    updateCartItemQuantity(id, -1);
}
