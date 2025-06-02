using System.Text;
using System.Text.Json;
using static UserStoryGenerator.Model.GFSGeminiClientHost;

namespace UserStoryGenerator.View
{
    public partial class MainForm : Form
    {
        private readonly UserStoryGenerator.Model.GFSGeminiClientHost gfsGeminiClientHost;

        private readonly Model.Model model;

        readonly string testRobustData = "{\"Issues\":[{\"Summary\":\"As a user, I want to browse products easily on my mobile device, so that I can quickly find the items I'm interested in.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop responsive UI components for product display (React/Angular/Vue)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement product search API endpoint (Node.js/Python)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Create 'Products' database table with columns: 'ProductID', 'Name', 'Description', 'Price', 'ImageURL', 'Category'\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Optimize product images for mobile devices\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement product filtering and sorting functionality\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Design mobile-first product listing page\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want to add products to a shopping cart, so that I can keep track of the items I want to purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop shopping cart component (React/Angular/Vue)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement 'Add to Cart' API endpoint (Node.js/Python)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement 'Shopping Cart' microservice API endpoints:  'AddItem', 'RemoveItem', 'ViewCart', 'UpdateQuantity'\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement cart persistence using local storage or cookies\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Ensure cart is accessible across devices\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want to complete secure online purchases, so that I can buy products with confidence.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Integrate with a secure payment gateway (Stripe, PayPal)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement SSL encryption for all payment-related pages\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement PCI DSS compliance\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Develop checkout form with address and payment details\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement order confirmation and email notifications\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Develop 'Orders' database table with columns: 'OrderID', 'CustomerID', 'OrderDate', 'TotalAmount', 'ShippingAddress', 'PaymentMethod', 'OrderStatus'\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want a streamlined checkout process, so that I can quickly and easily complete my purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Implement one-page checkout\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Enable guest checkout\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement address auto-completion\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Offer multiple shipping options\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Provide clear error messages and validation\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want diverse payment options, so that I can choose the most convenient method for me.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Integrate with multiple payment gateways (Credit Card, PayPal, Apple Pay, Google Pay)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement payment method selection on checkout page\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Handle different payment currencies\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Ensure secure storage of payment information (tokenization)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want robust order tracking capabilities, so that I can stay informed about the status of my shipment.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Integrate with shipping providers (UPS, FedEx, USPS)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement order tracking API endpoint (Node.js/Python)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Display order status and tracking information in user account\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Send email notifications for order updates\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want a user-friendly platform, so that I can easily navigate and use the online store.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Conduct usability testing with target users\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement clear and intuitive navigation\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Design a clean and modern user interface\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Provide helpful tooltips and instructions\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want the platform to be accessible, so that I can use the online store regardless of my abilities.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Ensure compliance with WCAG guidelines\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Provide alternative text for all images\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Ensure keyboard navigation is supported\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Provide sufficient color contrast\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want a highly performant platform, so that I can quickly browse and purchase products.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Optimize website loading speed\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Use a Content Delivery Network (CDN)\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Optimize database queries\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Monitor website performance and identify bottlenecks\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a user, I want a seamless user experience across all devices, so that I can shop comfortably on any device I choose.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Implement a responsive design that adapts to different screen sizes\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Test the platform on various devices and browsers\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Ensure consistent branding and design across all devices\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As an administrator, I want to create and manage product categories, so that users can easily find products.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop admin interface for category management\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Create 'Categories' database table with columns: 'CategoryID', 'Name', 'Description'\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement API endpoints for category creation, update, and deletion\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As an administrator, I want to manage customer data, so that I can understand user behavior and personalize experiences.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop admin interface for customer management\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Create a Database table called \\\"Customer\\\". Add the \\\"Name\\\" and \\\"Phone number\\\" columns to the \\\"Customer\\\" table.\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement API endpoints for customer data retrieval and updates\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a marketing manager, I want to create and manage promotions, so that I can attract more customers and increase sales.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop admin interface for promotion management\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Create 'Promotions' database table with columns: 'PromotionID', 'Name', 'Description', 'Discount', 'StartDate', 'EndDate'\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement API endpoints for promotion creation, update, and deletion\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a customer, I want to be able to view related product recommendations, so that I can discover new items I might like.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Implement recommendation engine based on purchase history or browsing behavior\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Develop API endpoint for retrieving product recommendations\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Display related products on product detail pages and in the shopping cart\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]},{\"Summary\":\"As a returning user, I want to be able to log in quickly and securely, so that I can access my account and saved information easily.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Implement user authentication and authorization\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Support login via email/password, social media accounts\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement password reset functionality\",\"IssueType\":\"Task\",\"Product\":\"GSL\"},{\"Summary\":\"Implement Two-Factor Authentication (2FA) for enhanced security\",\"IssueType\":\"Task\",\"Product\":\"GSL\"}]}]}";
        readonly string testRobustDataWithTests = "{\"Issues\":[{\"Summary\":\"As a customer, I want to browse products, so that I can discover items to purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop product listing microservice API endpoint\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop product listing microservice API endpoint\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Design and implement product display UI component (thumbnails, names, prices)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Design and implement product display UI component (thumbnails, names, prices)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement search and filter functionality on product listings\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement search and filter functionality on product listings\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Create 'Products' database table with columns: product_id, name, description, price, stock_quantity, image_url, category_id\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create 'Products' database table with columns: product_id, name, description, price, stock_quantity, image_url, category_id\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Create 'Categories' database table with columns: category_id, name\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create 'Categories' database table with columns: category_id, name\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop product detail microservice API endpoint\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop product detail microservice API endpoint\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement product detail page UI component (large image, detailed description, add to cart button)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement product detail page UI component (large image, detailed description, add to cart button)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]}]},{\"Summary\":\"As a customer, I want to add products to a shopping cart, so that I can gather items for purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop 'Add to Cart' microservice API endpoint (POST /cart/add)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Add to Cart' microservice API endpoint (POST /cart/add)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop 'Update Cart Item Quantity' microservice API endpoint (PUT /cart/update/{item_id})\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Update Cart Item Quantity' microservice API endpoint (PUT /cart/update/{item_id})\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop 'Remove from Cart' microservice API endpoint (DELETE /cart/remove/{item_id})\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Remove from Cart' microservice API endpoint (DELETE /cart/remove/{item_id})\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop 'Get Cart Contents' microservice API endpoint (GET /cart)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Get Cart Contents' microservice API endpoint (GET /cart)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement 'Add to Cart' button functionality on product pages\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement 'Add to Cart' button functionality on product pages\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Design and implement shopping cart UI (list of items, quantities, subtotals, total, checkout button)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Design and implement shopping cart UI (list of items, quantities, subtotals, total, checkout button)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Create 'ShoppingCarts' database table with columns: cart_id, user_id, created_at, updated_at\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create 'ShoppingCarts' database table with columns: cart_id, user_id, created_at, updated_at\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Create 'CartItems' database table with columns: cart_item_id, cart_id, product_id, quantity, price_at_time_of_addition\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create 'CartItems' database table with columns: cart_item_id, cart_id, product_id, quantity, price_at_time_of_addition\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]}]},{\"Summary\":\"As a customer, I want to complete secure online purchases, so that I can acquire products from the store.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop 'Initiate Checkout' microservice API endpoint (POST /checkout/initiate)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Initiate Checkout' microservice API endpoint (POST /checkout/initiate)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop 'Process Payment' microservice API endpoint (POST /payment/process)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Process Payment' microservice API endpoint (POST /payment/process)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop 'Create Order' microservice API endpoint (POST /orders)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Create Order' microservice API endpoint (POST /orders)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement checkout flow UI (shipping address, payment method selection, order summary)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement checkout flow UI (shipping address, payment method selection, order summary)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Integrate with payment gateway API (e.g., Stripe, PayPal)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Integrate with payment gateway API (e.g., Stripe, PayPal)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Create 'Orders' database table with columns: order_id, user_id, order_date, total_amount, shipping_address_id, billing_address_id, payment_status, order_status\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create 'Orders' database table with columns: order_id, user_id, order_date, total_amount, shipping_address_id, billing_address_id, payment_status, order_status\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Create 'OrderItems' database table with columns: order_item_id, order_id, product_id, quantity, unit_price\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create 'OrderItems' database table with columns: order_item_id, order_id, product_id, quantity, unit_price\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Create 'Payments' database table with columns: payment_id, order_id, transaction_id, payment_method, amount, payment_date, status\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create 'Payments' database table with columns: payment_id, order_id, transaction_id, payment_method, amount, payment_date, status\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]}]},{\"Summary\":\"As a customer, I want to track my orders, so that I can know the status of my purchases.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Develop 'Get Order History' microservice API endpoint (GET /orders/history)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Get Order History' microservice API endpoint (GET /orders/history)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop 'Get Order Details' microservice API endpoint (GET /orders/{order_id})\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Get Order Details' microservice API endpoint (GET /orders/{order_id})\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement order history UI (list of past orders with status)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement order history UI (list of past orders with status)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement order detail UI (detailed order information, tracking updates)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement order detail UI (detailed order information, tracking updates)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Create 'Shipments' database table with columns: shipment_id, order_id, tracking_number, carrier, shipment_date, estimated_delivery_date, status\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create 'Shipments' database table with columns: shipment_id, order_id, tracking_number, carrier, shipment_date, estimated_delivery_date, status\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]}]},{\"Summary\":\"As a user, I want a mobile-first responsive platform, so that I can access the store seamlessly on any device.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Implement responsive design principles using CSS media queries and flexible layouts\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement responsive design principles using CSS media queries and flexible layouts\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Conduct cross-browser and cross-device compatibility testing\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Conduct cross-browser and cross-device compatibility testing\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Optimize image assets for various screen resolutions\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Optimize image assets for various screen resolutions\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]}]},{\"Summary\":\"As a user, I want a user-friendly and accessible platform, so that I can easily navigate and interact with the store.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Perform usability testing with target user groups\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Perform usability testing with target user groups\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement WCAG 2.1 accessibility guidelines (e.g., semantic HTML, ARIA attributes, keyboard navigation)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement WCAG 2.1 accessibility guidelines (e.g., semantic HTML, ARIA attributes, keyboard navigation)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Conduct accessibility audits using automated tools and manual review\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Conduct accessibility audits using automated tools and manual review\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]}]},{\"Summary\":\"As a user, I want a highly performant platform, so that I experience fast loading times and smooth interactions.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Optimize frontend assets (minify CSS/JS, compress images)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Optimize frontend assets (minify CSS/JS, compress images)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement caching mechanisms for API responses and static content\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement caching mechanisms for API responses and static content\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Optimize database queries and indexing\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Optimize database queries and indexing\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Conduct load testing and performance benchmarks\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Conduct load testing and performance benchmarks\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]}]},{\"Summary\":\"As a store administrator, I want to manage customer data, so that I can provide personalized support and insights.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Create Database table called \\\"Customer\\\" with columns: customer_id, name, phone_number, email, shipping_address, billing_address, registration_date\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Create Database table called \\\"Customer\\\" with columns: customer_id, name, phone_number, email, shipping_address, billing_address, registration_date\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop 'Get Customer Details' microservice API endpoint (GET /customers/{customer_id})\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Get Customer Details' microservice API endpoint (GET /customers/{customer_id})\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Develop 'Update Customer Details' microservice API endpoint (PUT /customers/{customer_id})\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Develop 'Update Customer Details' microservice API endpoint (PUT /customers/{customer_id})\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]},{\"Summary\":\"Implement customer management UI for administrators (view, edit customer profiles)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"LinkedIssues\":[{\"Summary\":\"Test for Task: Implement customer management UI for administrators (view, edit customer profiles)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"LinkedIssues\":[]}]}]}]}";
        readonly string testRobustDataWithTestsAndSubTasks = "{\"Issues\":[{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Implement product browsing functionality in the frontend using React components and Redux for state management.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3424135030},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for React components.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1839318669},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for Redux state management.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1823730019}],\"Summary\":\"Implement product browsing functionality in the frontend using React components and Redux for state management.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":926781454},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Create a REST API endpoint for fetching products with pagination and filtering.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3341678192},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for API endpoint logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2145957325},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for API endpoint with database.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":3295601589}],\"Summary\":\"Create a REST API endpoint for fetching products with pagination and filtering.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2855318941},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Design and implement a database schema for products including fields like name, description, price, and image URL.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2255268903},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for database models.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":793985085},{\"LinkedIssues\":null,\"Summary\":\"Write migration tests for database schema.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":931330289}],\"Summary\":\"Design and implement a database schema for products including fields like name, description, price, and image URL.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":3553520492}],\"Summary\":\"As a user, I want to browse products, so that I can find items I want to purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":3045851551},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Implement shopping cart functionality in the frontend using local storage or cookies.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1010449970},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for cart management logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2820613069},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for cart persistence.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":3464107170}],\"Summary\":\"Implement shopping cart functionality in the frontend using local storage or cookies.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":852424609},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Create API endpoints for adding, updating, and removing products from the shopping cart.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":425129035},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for cart API endpoint logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2876339659},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for cart API endpoints with database.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":3373873567}],\"Summary\":\"Create API endpoints for adding, updating, and removing products from the shopping cart.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":4265695388},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Update the database schema to include a shopping cart table linked to user accounts.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":244230597},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for database cart models.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":4286005628},{\"LinkedIssues\":null,\"Summary\":\"Write migration tests for cart database schema.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1991742461}],\"Summary\":\"Update the database schema to include a shopping cart table linked to user accounts.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2041565198}],\"Summary\":\"As a user, I want to add products to a shopping cart, so that I can collect the items I want to purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":1703312840},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Implement a streamlined checkout process in the frontend using React components and a payment gateway integration.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2370945912},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for checkout form components.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2199737101},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for payment gateway integration.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":986893531}],\"Summary\":\"Implement a streamlined checkout process in the frontend using React components and a payment gateway integration.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1556041641},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Create API endpoints for processing payments using a secure payment gateway (e.g., Stripe, PayPal).\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2258680067},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for payment processing logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":283533164},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for payment gateway API.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1622959819}],\"Summary\":\"Create API endpoints for processing payments using a secure payment gateway (e.g., Stripe, PayPal).\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2922045532},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Update the database schema to include order information and payment details.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2243490340},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for database order models.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":3429484599},{\"LinkedIssues\":null,\"Summary\":\"Write migration tests for order database schema.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2350306832}],\"Summary\":\"Update the database schema to include order information and payment details.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1858433809}],\"Summary\":\"As a user, I want to complete secure online purchases, so that I can buy the items in my shopping cart.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":1708852835},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Integrate multiple payment gateways (e.g., credit cards, PayPal, Apple Pay) in the frontend checkout.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":304506785},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for payment method selection logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":944176551},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for each payment gateway.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":278851091}],\"Summary\":\"Integrate multiple payment gateways (e.g., credit cards, PayPal, Apple Pay) in the frontend checkout.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":266310261},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Update the payment processing API to handle different payment methods.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1350335089},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for payment method API logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":731947974},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for each payment method's API.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2101662750}],\"Summary\":\"Update the payment processing API to handle different payment methods.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2600578898}],\"Summary\":\"As a user, I want diverse payment options, so that I can pay using my preferred method.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":1729643222},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Implement order tracking functionality in the frontend to display order status and shipment details.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2238435270},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for order tracking components.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":604539666},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for order status updates.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1377407695}],\"Summary\":\"Implement order tracking functionality in the frontend to display order status and shipment details.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2115451980},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Create API endpoints for retrieving order information and tracking details from shipping providers.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":539497888},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for order tracking API logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2051959814},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for shipping provider APIs.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1331618930}],\"Summary\":\"Create API endpoints for retrieving order information and tracking details from shipping providers.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2508087827},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Update the database schema to include order status and tracking information.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2416067089},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for database order tracking models.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":654794176},{\"LinkedIssues\":null,\"Summary\":\"Write migration tests for order tracking database schema.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":3725067829}],\"Summary\":\"Update the database schema to include order status and tracking information.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1259250572}],\"Summary\":\"As a user, I want robust order tracking capabilities, so that I can monitor the status of my purchases.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":1832124812},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Develop a consistent and intuitive user interface using a UI framework like Material-UI or Bootstrap.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":4127205812},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for UI components.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2637178515},{\"LinkedIssues\":null,\"Summary\":\"Perform accessibility testing for UI components.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2121425811}],\"Summary\":\"Develop a consistent and intuitive user interface using a UI framework like Material-UI or Bootstrap.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":442559400},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Conduct usability testing to gather feedback on the platform's user-friendliness.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3046303568}],\"Summary\":\"Conduct usability testing to gather feedback on the platform's user-friendliness.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2801379944}],\"Summary\":\"As a user, I want a user-friendly platform, so that I can easily navigate and use the online store.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":318870113},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Implement accessibility features, such as ARIA attributes and keyboard navigation, to comply with WCAG guidelines.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":950006162},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for accessibility features.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":121009203},{\"LinkedIssues\":null,\"Summary\":\"Use accessibility testing tools to validate compliance.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":765226893}],\"Summary\":\"Implement accessibility features, such as ARIA attributes and keyboard navigation, to comply with WCAG guidelines.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1403157367},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Perform accessibility audits to identify and resolve accessibility issues.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":4160201776}],\"Summary\":\"Perform accessibility audits to identify and resolve accessibility issues.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":3743687601}],\"Summary\":\"As a user, I want an accessible platform, so that I can use the online store regardless of my abilities.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":1433293800},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Optimize the platform's performance by implementing caching strategies and minimizing HTTP requests.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3789762188},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for caching logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2292928724},{\"LinkedIssues\":null,\"Summary\":\"Perform load testing to identify performance bottlenecks.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":93575749}],\"Summary\":\"Optimize the platform's performance by implementing caching strategies and minimizing HTTP requests.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1987316864},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Monitor the platform's performance using tools like New Relic or Datadog to identify and resolve performance issues.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2306775554}],\"Summary\":\"Monitor the platform's performance using tools like New Relic or Datadog to identify and resolve performance issues.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":514007367}],\"Summary\":\"As a user, I want a highly performant platform, so that I can quickly browse and complete purchases.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":2397695496},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Implement a responsive design using CSS media queries to ensure the platform adapts to different screen sizes.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":697229827},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for responsive CSS.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2813302435},{\"LinkedIssues\":null,\"Summary\":\"Test the platform on different devices and browsers.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":3520888089}],\"Summary\":\"Implement a responsive design using CSS media queries to ensure the platform adapts to different screen sizes.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":3781249024},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Test the platform on different devices and browsers to ensure a consistent user experience.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2033245194}],\"Summary\":\"Test the platform on different devices and browsers to ensure a consistent user experience.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":296961118}],\"Summary\":\"As a user, I want a seamless user experience across all devices, so that I can use the online store on my desktop, tablet, or phone.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":1942911521},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Design and create the 'Customer' database table with columns for 'Name' (VARCHAR), 'Phone number' (VARCHAR), 'Email' (VARCHAR), 'Address' (VARCHAR), and 'CustomerID' (INT, Primary Key, Auto-increment).\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3878905315},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for Customer database model.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1964702006},{\"LinkedIssues\":null,\"Summary\":\"Write migration tests for customer database schema.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2321078634}],\"Summary\":\"Design and create the 'Customer' database table with columns for 'Name' (VARCHAR), 'Phone number' (VARCHAR), 'Email' (VARCHAR), 'Address' (VARCHAR), and 'CustomerID' (INT, Primary Key, Auto-increment).\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2659174462},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Create API endpoint for managing customer data\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3197919926},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for customer API endpoint logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":2100339882},{\"LinkedIssues\":null,\"Summary\":\"Write integration tests for customer API endpoints with database.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1219525953}],\"Summary\":\"Create API endpoint for managing customer data\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2759933354}],\"Summary\":\"Create a Database table called \\\"Customer\\\". Add the \\\"Name\\\" and \\\"Phone number\\\" and other relevant columns to the \\\"Customer\\\" table.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":4112672781},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Implement encryption for sensitive data like passwords and payment information.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3075719302},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for encryption functions.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":117758370},{\"LinkedIssues\":null,\"Summary\":\"Perform penetration testing to identify vulnerabilities.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":111630362}],\"Summary\":\"Implement encryption for sensitive data like passwords and payment information.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":3757345746},{\"LinkedIssues\":[{\"LinkedIssues\":null,\"Summary\":\"QA Test for Task: Enforce strong password policies and two-factor authentication.\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2245865214},{\"LinkedIssues\":null,\"Summary\":\"Write unit tests for password validation logic.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1662710243},{\"LinkedIssues\":null,\"Summary\":\"Implement and test two-factor authentication workflow.\",\"IssueType\":\"Sub-task\",\"Product\":\"GSL\",\"Key\":1053218633}],\"Summary\":\"Enforce strong password policies and two-factor authentication.\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":2592747338}],\"Summary\":\"As a developer, I want to ensure data security, so that customer information is protected.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":4259803851}]}";
        readonly string testShortDataWithTests = "{\"Issues\":[{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Develop product listing microservice API endpoint\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1413804073}],\"Summary\":\"Develop product listing microservice API endpoint\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1311091604},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Design and implement product display UI component (thumbnails, names, prices)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1603084279}],\"Summary\":\"Design and implement product display UI component (thumbnails, names, prices)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":786585654},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Implement search and filter functionality on product listings\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":619773154}],\"Summary\":\"Implement search and filter functionality on product listings\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1892422913},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Create 'Products' database table with columns: product_id, name, description, price, stock_quantity, image_url, category_id\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3651290013}],\"Summary\":\"Create 'Products' database table with columns: product_id, name, description, price, stock_quantity, image_url, category_id\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":3478830013},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Create 'Categories' database table with columns: category_id, name\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1034073894}],\"Summary\":\"Create 'Categories' database table with columns: category_id, name\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1017039075},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Develop product detail microservice API endpoint\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":611511485}],\"Summary\":\"Develop product detail microservice API endpoint\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":334874111},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Implement product detail page UI component (large image, detailed description, add to cart button)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1833606298}],\"Summary\":\"Implement product detail page UI component (large image, detailed description, add to cart button)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":341566955}],\"Summary\":\"As a customer, I want to browse products, so that I can discover items to purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":2726800006},{\"LinkedIssues\":[{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Develop 'Add to Cart' microservice API endpoint (POST /cart/add)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1561982148}],\"Summary\":\"Develop 'Add to Cart' microservice API endpoint (POST /cart/add)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":4107878204},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Develop 'Update Cart Item Quantity' microservice API endpoint (PUT /cart/update/{item_id})\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":3026138080}],\"Summary\":\"Develop 'Update Cart Item Quantity' microservice API endpoint (PUT /cart/update/{item_id})\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":962148599},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Develop 'Remove from Cart' microservice API endpoint (DELETE /cart/remove/{item_id})\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2268157038}],\"Summary\":\"Develop 'Remove from Cart' microservice API endpoint (DELETE /cart/remove/{item_id})\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":3733928180},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Develop 'Get Cart Contents' microservice API endpoint (GET /cart)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2987220198}],\"Summary\":\"Develop 'Get Cart Contents' microservice API endpoint (GET /cart)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":388050370},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Implement 'Add to Cart' button functionality on product pages\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2454933550}],\"Summary\":\"Implement 'Add to Cart' button functionality on product pages\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":417817911},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Design and implement shopping cart UI (list of items, quantities, subtotals, total, checkout button)\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":1263142961}],\"Summary\":\"Design and implement shopping cart UI (list of items, quantities, subtotals, total, checkout button)\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1139981311},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Create 'ShoppingCarts' database table with columns: cart_id, user_id, created_at, updated_at\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2328263046}],\"Summary\":\"Create 'ShoppingCarts' database table with columns: cart_id, user_id, created_at, updated_at\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":3459179441},{\"LinkedIssues\":[{\"LinkedIssues\":[],\"Summary\":\"Test for Task: Create 'CartItems' database table with columns: cart_item_id, cart_id, product_id, quantity, price_at_time_of_addition\",\"IssueType\":\"Test\",\"Product\":\"GSL\",\"Key\":2242483846}],\"Summary\":\"Create 'CartItems' database table with columns: cart_item_id, cart_id, product_id, quantity, price_at_time_of_addition\",\"IssueType\":\"Task\",\"Product\":\"GSL\",\"Key\":1594362981}],\"Summary\":\"As a customer, I want to add products to a shopping cart, so that I can gather items for purchase.\",\"IssueType\":\"Story\",\"Product\":\"GSL\",\"Key\":3501738917}]}";


        public MainForm()
        {
            model = new();
            if( model.Settings == null ) throw new NullReferenceException(nameof(model.Settings));
            if( model.Settings.Key == null ) throw new NullReferenceException(nameof(model.Settings.Key));


            InitializeComponent();

            treeView.TriStateStyleProperty = TriStateTreeView.TriStateStyles.Standard;

            gfsGeminiClientHost = new(model.Settings.Key, AIType.GenerativeAI);
            gfsGeminiClientHost.LookupCompleted += GfsGeminiClientHost_LookupCompleted;

            // testing prefils
            textBoxText.Text = "Implement a mobile-first, fully responsive e-commerce platform for our online store, enabling users to browse products, add them to a shopping cart, and complete secure online purchases. The platform should offer a streamlined checkout process, diverse payment options, and robust order tracking capabilities. We need to ensure that the platform is user-friendly, accessible, and highly performant, with a focus on a seamless user experience across all devices.Please include this task: Create a Database table called \"Customer\".   Add the \"Name\" and \"Phone number\" and other relevant columns to the \"Customer\" table.";

            this.textBoxProduct.Text = "GSL";
            this.textBoxEpic.Text = "An epic Epic";
            checkBoxAddQATests.Checked = true;

            TestAndFill(testRobustDataWithTestsAndSubTasks);
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            treeView.Checked += (number) =>
            {
                buttonSave.Enabled = !( number == 0 );
            };

            treeView.ExpandAll();

            if( treeView.Nodes.Count > 0 )
                treeView.TopNode = treeView.Nodes[0];

        }

        private async void Convert_Click(object? sender, EventArgs e)
        {
            treeView.Nodes.Clear();
            progressBar.Visible = true;

            await RequestAnswer();
            //
        }

        private async Task RequestAnswer()
        {
            gfsGeminiClientHost.Query = BuildQuery();

            await gfsGeminiClientHost.RequestAnswer();

        }

        private string BuildQuery()
        {
            StringBuilder sb = new();

            sb.AppendLine("Analyze the following detailed software develoment feature product description to identify and create as many distinct agile scrum user stories embedded within the text For each, generate a Jira issue summary line following the standard Agile User Story format: 'As a [user], I want [action], so that [benefit]'");

            sb.AppendLine("Be creative, read between the lines to imagine robust plentiful 'inbetween' functionalitie.s");

            sb.AppendLine("Provide the response as a collection of issue objects in JSON format.  The json schema is: 'Issues' :[]");
            sb.AppendLine("The Issue schema is: { 'Summary', 'IssueType', Product', 'LinkedIssues': [] }");
            sb.AppendLine("Issue 'IssueType' can be 'Story', 'Task', 'Test', 'Defect', 'Sub-task'");
            sb.AppendLine("Issue 'Product' is the Product specified in the text.");
            sb.AppendLine("LinkedIssues is a collection are Issue objects set with the same 'Product' value as their parent.");

            sb.AppendLine("Whereever possible, add a robust, encompassing collection of issues (LinkedIssues) with Issue 'IssueType' = 'Task' for all backend, frontend, database, and API layers to the User story issues. Don't follow the agile user story format for the tasks, be as technical as possible.");

            sb.AppendLine("Be creative and Imagine the need for a robust array of microservice API endpoints. Database Tables and with many specified columns to add.  Many common User Interface controls needed in the frontend.");


            sb.AppendLine("All LinkedIssues must have their 'Product' value set to the Product specified in the Text.");

            if( checkBoxAddQATests.Checked )
            {
                sb.AppendLine("For every 'Task' issue, create an issue of IssueType = 'Test' and a 'Summary' that is 'QA Test for Task, concatenated with the Task's Summary.");
                sb.AppendLine("The 'Test' issues are members of their parent Task's 'LinkedIssues' collection.");
            }

            if( checkBoxAddUnitTests.Checked )
            {
                sb.AppendLine("Every 'Task' issue (not the 'Test' issues) should have several Sub-tasks regarding Test Driven Development (TDD) test for the Task.  The Sub-task should go into the Task's (not the Test) LinkedIssues collection.");
            }

            sb.AppendLine($"Product:{this.textBoxProduct.Text}");
            sb.AppendLine("Product description:");

            sb.AppendLine(textBoxText.Text);

            return sb.ToString();
        }

        private void GfsGeminiClientHost_LookupCompleted()
        {
            if( this.treeView.InvokeRequired )
                this.Invoke(new System.Windows.Forms.MethodInvoker(() => { GfsGeminiClientHost_LookupCompleted(); return; }));

            else
            {
                progressBar.Visible = false;

                IList<string>? answers = gfsGeminiClientHost.Answers;
                if( answers == null || answers.Count == 0 ) return;

                string answer = answers[0];
                //Utilities.Logger.Info(answer);
                try
                {
                    Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(answer);
                    Utilities.Logger.Info(JsonSerializer.Serialize(data));
                    if( data != null )
                        PopulateTree(data);

                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void TestAndFill(string json)
        {
            try
            {
                //Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(testData);
                Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(json);
                string result = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = false });
                Utilities.Logger.Info(result);
                if( data != null )
                    PopulateTree(data);

            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PopulateTree(Model.IssueData data)
        {
            if( data.Issues == null ) return;
            treeView.Nodes.Clear();

            TreeNode? root = new(this.textBoxEpic.Text);
            treeView.Nodes.Add(root);

            Recursive(data.Issues, root);

            //root.Checked = true;            //treeView.SelectAllNodes(false);
            treeView.ExpandAll();
            if( treeView.Nodes.Count > 0 )
                treeView.TopNode = treeView.Nodes[0];
            //
        }

        private void Recursive(IList<Model.IssueData.Issue> issues, TreeNode node)
        {
            foreach( Model.IssueData.Issue issue in issues )
            {
                if( issue.Summary == null ) continue;
                TriStateTreeView.TreeNodeEx? newNode = new(issue);
                newNode.ToolTipText = issue.IssueType;
                switch( issue.IssueType )
                {
                    case "Story":
                        newNode.ForeColor = Color.DarkGreen;
                        break;
                    case "Task":
                        newNode.ForeColor = Color.Navy;
                        break;
                    case "Defect":
                        newNode.ForeColor = Color.IndianRed;
                        break;
                    case "Test":
                        newNode.ForeColor = Color.DarkGoldenrod;
                        break;
                    case "Sub-task":
                        newNode.ForeColor = Color.DarkSalmon;
                        break;
                    default:
                        newNode.ForeColor = Color.Black;
                        break;
                }
                node.Nodes.Add(newNode);

                if( issue.LinkedIssues != null )
                    Recursive(issue.LinkedIssues, newNode);
            }
        }

        private void FillUIDEP(Model.IssueData data)
        {
            if( data.Issues == null ) return;
            treeView.Nodes.Clear();

            TreeNode? root = new("Issues");
            treeView.Nodes.Add(root);

            foreach( Model.IssueData.Issue issue in data.Issues )
            {
                if( issue.Summary == null ) continue;

                TriStateTreeView.TreeNodeEx? node = new(issue);
                root.Nodes.Add(node);

                if( issue.LinkedIssues != null )
                {
                    foreach( var linkedissue in issue.LinkedIssues )
                    {
                        if( linkedissue != null )
                        {
                            TriStateTreeView.TreeNodeEx? linkedNode = new(linkedissue);
                            node.Nodes.Add(linkedNode);

                            if( linkedissue.LinkedIssues != null )
                            {
                                foreach( var sublinkedissue in linkedissue.LinkedIssues )
                                {
                                    TriStateTreeView.TreeNodeEx? linkedlinkedNode = new(linkedissue);
                                    //linkedNode.Nodes.Add(sublinkedissue);
                                }
                            }
                        }
                    }
                }
            }

            //root.Checked = true;            //treeView.SelectAllNodes(false);
            treeView.ExpandAll();
            if( treeView.Nodes.Count > 0 )
                treeView.TopNode = treeView.Nodes[0];
            //
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            List<string> storyList = treeView.GetCheckedNodes();
            List<TreeNode> checkedHierarchy = treeView.GetCheckedNodesHierarchy(true);

            if( storyList.Count == 0 )
            {
                MessageBox.Show(
                    "No selections were made",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );

                return;
                //
            }

            model.SaveDataToFile(this.textBoxEpic.Text, storyList, checkedHierarchy);
            //
        }

        private void TextControls_TextChanged(object? sender, EventArgs e)
        {
            buttonConvert.Enabled = !( textBoxText.TextLength == 0 || textBoxProduct.TextLength == 0 );
        }
        private void TextBoxEpic_TextChanged(object sender, EventArgs e)
        {
            string currentText = this.textBoxEpic.Text;

            if( currentText.Length == 0 )//|| currentText.Length> 8
            {
                textBoxEpic.ForeColor = SystemColors.ControlText;
                TextControls_TextChanged(sender, new EventArgs());
                return;
            }

            bool found = Utilities.InputValidator.RegexContainsValidation(currentText);
            if( found )
            {
                bool isValid = Utilities.InputValidator.RegexValidation(currentText);

                // Update the label based on the validation result
                if( isValid )
                {
                    textBoxEpic.ForeColor = SystemColors.ControlText;
                    TextControls_TextChanged(sender, new EventArgs());
                    return;
                }
                else
                {
                    textBoxEpic.ForeColor = System.Drawing.Color.Red;
                    buttonConvert.Enabled = false;
                    return;
                }
            }
            else
            {
                textBoxEpic.ForeColor = SystemColors.ControlText;
                TextControls_TextChanged(sender, new EventArgs());
            }
        }

        private void CheckBoxAddUnitTests_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBoxAddQATests_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
