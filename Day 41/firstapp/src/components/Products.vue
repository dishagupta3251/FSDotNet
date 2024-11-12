<script>
export default {
    name: "ProductList",
    data() {
        return {
            products: [],
            categories: [],
            filterProducts:[],
            selectedCategory: "",
            btnClick: () => {
                fetch('https://dummyjson.com/products')
                    .then(res => res.json())
                    .then(data => {
                        console.log(data.products);
                        this.products = data.products
                        this.filterProducts=data.products
                    });
            },
            selectCategory: () => {
                console.log(this.selectedCategory)
                if (this.selectedCategory) {
                        this.filterProducts = this.products.filter(product => product.category === this.selectedCategory);
                } else {
                    this.btnClick(); // Re-fetch products if no category is selected
                }
            }
        }
    },
    mounted() {
        fetch('https://dummyjson.com/products/category-list')
            .then(res => res.json())
            .then(data => {
                console.log(data);
                this.categories = data
            });
        fetch('https://dummyjson.com/products')
            .then(res => res.json())
            .then(data => {
                console.log(data.products);
                this.products = data.products
                this.filterProducts=data.products
            });
    }
}
 
</script>
<template>
<section>
<h1>Products</h1>
 
        <button @click="btnClick()" class="btn btn-success">Click me</button>
<select v-model="selectedCategory" @change="selectCategory" name="Category" placeholder="Select a category"
            id="">
<option>Select a Category</option>
 
            <option v-for="category in categories" :key="category" :value="category">{{ category }}</option>
</select>
 
        <div v-if="products.length > 0">
<h2>List</h2>
<div v-for="product in filterProducts" :key="product.id" class="card proddiv">
<img class="card-img-top" :src=product.thumbnail alt="Card image cap">
<div class="card-body">
<h5 class="card-title">{{ product.title }}</h5>
<p class="card-text">{{ product.description }}</p>
<button @click="btnClick(product.id)" class="btn btn-primary">buy @ {{ product.price }}</button>
</div>
</div>
</div>
 
    </section>
</template>
<style>
.proddiv {
    width: 20rem;
    position: relative;
    float: left;
    margin: 5px;
}
</style>

has context menu