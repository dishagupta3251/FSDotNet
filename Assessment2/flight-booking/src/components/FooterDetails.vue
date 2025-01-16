<template>
    <footer class="footer">
        <div class="footer-container">

            <div class="footer-section">
            </div>


            <div class="footer-section">
                <h3>About Flight</h3>
                <ul>
                    <li><a href="#">About us</a></li>
                    <li><a href="#">Investor Relations</a></li>
                    <li><a href="#">Contact us</a></li>
                    <li><a href="#">Mobile version</a></li>
                    <li><a href="#">Mobile</a></li>
                    <li><a href="#">Sitemap</a></li>
                    <li><a href="#">Offers</a></li>
                    <li><a href="#">Careers</a></li>
                    <li><a href="#">Values</a></li>
                </ul>
            </div>


            <div class="footer-section">
                <h3>Info</h3>
                <ul>
                    <li><a href="#">T&C</a></li>
                    <li><a href="#">Privacy policy</a></li>
                    <li><a href="#">FAQ</a></li>
                    <li><a href="#">Blog</a></li>
                    <li><a href="#">Agent registration</a></li>
                    <li><a href="#">Insurance partner</a></li>
                    <li><a href="#">User agreement</a></li>
                    <li><a href="#">Primo Flight</a></li>
                    <li><a href="#">Flight Timetable</a></li>
                </ul>
            </div>


            <div class="footer-section">
                <h3>Global Sites</h3>
                <ul>
                    <li><a href="#">India</a></li>
                    <li><a href="#">Singapore</a></li>
                    <li><a href="#">Malaysia</a></li>
                    <li><a href="#">Indonesia</a></li>
                    <li><a href="#">Peru</a></li>
                    <li><a href="#">Colombia</a></li>
                    <li><a href="#">Cambodia</a></li>
                    <li><a href="#">Vietnam</a></li>
                </ul>
            </div>

            <div class="footer-section">
                <h3>Our Partners</h3>
                <ul>
                    <li><a href="#">Flights</a></li>
                    <li><a href="#">Hotels</a></li>
                    <li><a href="#">Makemytrip Hotels</a></li>
                </ul>
                <div class="social-icons">
                    <a href="#"><i class="fab fa-facebook-f"></i></a>
                    <a href="#"><i class="fab fa-linkedin-in"></i></a>
                    <a href="#"><i class="fab fa-twitter"></i></a>
                    <a href="#"><i class="fab fa-instagram"></i></a>
                </div>
            </div>
        </div>
        <div class="footer-bottom">
            <p>© 2024 Flight India Pvt Ltd. All rights reserved</p>
        </div>
    </footer>
</template>

<script>
export default {
    name: "FooterDetails",
};
</script>

<style scoped>
.footer {
    background-color: #f9f9f9;
    margin-top: 500px;
    padding: 20px 0;
    border-top: 1px solid #ddd;
}

.footer-container {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
    gap: 20px;
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
}

.footer-section {
    text-align: left;
}

.footer-section h3 {
    font-size: 18px;
    margin-bottom: 10px;
}

.footer-section p {
    font-size: 14px;
    line-height: 1.6;
    color: #555;
}

.footer-section ul {
    list-style: none;
    padding: 0;
}

.footer-section ul li {
    margin-bottom: 8px;
}

.footer-section ul li a {
    text-decoration: none;
    color: #555;
    font-size: 14px;
}

.footer-section ul li a:hover {
    color: #007bff;
}

.social-icons {
    display: flex;
    gap: 10px;
    margin-top: 10px;
}

.social-icons a {
    color: #555;
    font-size: 16px;
    text-decoration: none;
}

.social-icons a:hover {
    color: #007bff;
}

.footer-bottom {
    text-align: center;
    padding: 10px 0;
    background-color: #f1f1f1;
    font-size: 14px;
    color: #555;
}

@media (max-width: 768px) {
    .footer-container {
        grid-template-columns: 1fr;
    }
}
</style>