import { Component, OnInit } from '@angular/core';
import { CatelogItem, CatelogItemRepository, Stock, CardComponent, LoaderComponent } from '@shared';

@Component({
  selector: 'app-home-page',
  imports: [LoaderComponent, CardComponent],
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit {
  public items: CatelogItem[] = [
    {
      "id": "e7aa745d-fab8-4ab8-bca2-92e6fbb0f198",
      "sku": "2064706770719",
      "name": "Practical Frozen Pizza",
      "description": "Sit deleniti quia autem. Ab quo optio. Fugit voluptatem alias.",
      "price": 835.3335354921726,
      "quantity": 60,
      "tags": [
        "Clothing",
        "Kids",
        "Books"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=597"
    },
    {
      "id": "b5b33a9a-0ef6-4dc5-b4fa-75ba2c376fce",
      "sku": "8737865483649",
      "name": "Fantastic Metal Gloves",
      "description": "Natus sit sunt minus vel eos sed eos facere repellat. Et fugiat voluptates est ea et. Vel omnis quis omnis voluptatem aperiam voluptas sed. Omnis ab minus voluptatem delectus eum architecto nam in. Eveniet maxime quibusdam. Minus deleniti sequi at maiores.",
      "price": 823.8937741591122,
      "quantity": 73,
      "tags": [
        "Outdoors",
        "Beauty",
        "Shoes"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=701"
    },
    {
      "id": "61917343-83ff-4b05-aaf5-c185d250b91f",
      "sku": "1248983611663",
      "name": "Incredible Fresh Ball",
      "description": "Fugit enim omnis molestiae cumque aut. Quibusdam non et et aliquid et qui sequi et voluptatibus. At et quos. Voluptatum commodi ut laudantium eos autem. Mollitia necessitatibus sit nesciunt blanditiis ullam tempore minima non animi.",
      "price": 68.67192757996409,
      "quantity": 75,
      "tags": [
        "Shoes",
        "Books",
        "Baby"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=420"
    },
    {
      "id": "ed134066-f063-409c-b932-a74cfc0f2191",
      "sku": "8544755459093",
      "name": "Ergonomic Concrete Chips",
      "description": "Nihil quidem voluptas officia et. Quas animi dolorem iste ut. Est molestiae voluptatum quibusdam aut aliquam officia delectus rerum iste.",
      "price": 53.794719979599655,
      "quantity": 61,
      "tags": [
        "Home",
        "Garden",
        "Movies"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=958"
    },
    {
      "id": "241c0871-93a6-41c5-b85e-e14689c59f29",
      "sku": "1538611772058",
      "name": "Practical Cotton Pizza",
      "description": "Exercitationem natus adipisci incidunt voluptas consequatur consectetur error enim. Vel ut laborum ratione qui voluptatem. Ut dolorum at itaque. Omnis consequatur rerum. Et vero culpa. Sed hic ducimus quae aut est.",
      "price": 210.64672844310687,
      "quantity": 19,
      "tags": [
        "Music",
        "Grocery",
        "Electronics"
      ],
      "isDiscounted": true,
      "discountPercentage": 47.488210923836874,
      "imageUrl": "https://picsum.photos/640/480/?image=427"
    },
    {
      "id": "251cf236-e9be-4099-8aa5-0f97eff64ced",
      "sku": "8690814443753",
      "name": "Unbranded Rubber Tuna",
      "description": "Esse neque iure adipisci. Quod facere quas quasi velit. Fugit soluta nisi qui. Eligendi suscipit quia quia esse sed.",
      "price": 234.98194550129904,
      "quantity": 89,
      "tags": [
        "Music",
        "Home",
        "Music"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=541"
    },
    {
      "id": "e2c8d5cb-6635-481c-a135-69b8dc787728",
      "sku": "9579681340701",
      "name": "Generic Fresh Fish",
      "description": "Animi laboriosam neque sed dolorem sed id eveniet consectetur natus. Doloremque quia perspiciatis odio quidem. Et nesciunt hic inventore quam. Exercitationem odit ad iusto autem. Eos praesentium laborum cum est et omnis.",
      "price": 289.1624243531326,
      "quantity": 71,
      "tags": [
        "Electronics",
        "Beauty",
        "Computers"
      ],
      "isDiscounted": true,
      "discountPercentage": 13.408649641088516,
      "imageUrl": "https://picsum.photos/640/480/?image=181"
    },
    {
      "id": "87ee42f8-5f99-4434-abdd-41893355f791",
      "sku": "8234889850247",
      "name": "Fantastic Concrete Gloves",
      "description": "Iste et est adipisci commodi. Fugiat laborum accusamus dolorem. Sint itaque illo odit nihil in voluptatibus tempora alias voluptatem. Id animi sequi error. Illum sed aperiam cupiditate. Ea praesentium eveniet a quos beatae voluptates.",
      "price": 169.03197040025816,
      "quantity": 75,
      "tags": [
        "Computers",
        "Jewelery",
        "Books"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=244"
    },
    {
      "id": "60b91af9-89fa-4eee-b09c-639cea7cd770",
      "sku": "0517540633754",
      "name": "Tasty Concrete Shoes",
      "description": "Delectus et quia. Sint laborum sint tenetur quam nobis delectus qui distinctio sit. Voluptas nam aut labore. Sit veritatis omnis similique illum iste odit.",
      "price": 87.24428477954505,
      "quantity": 6,
      "tags": [
        "Tools",
        "Beauty",
        "Games"
      ],
      "isDiscounted": true,
      "discountPercentage": 8.669503371139243,
      "imageUrl": "https://picsum.photos/640/480/?image=27"
    },
    {
      "id": "bcb5f1d2-637f-4f45-944f-46a5ae726dfa",
      "sku": "2613863507623",
      "name": "Fantastic Wooden Computer",
      "description": "Sit nobis non ratione corrupti culpa. Distinctio magnam labore. Veritatis suscipit ut iusto aut sint.",
      "price": 432.2050121077135,
      "quantity": 28,
      "tags": [
        "Clothing",
        "Health",
        "Health"
      ],
      "isDiscounted": true,
      "discountPercentage": 17.546696952201806,
      "imageUrl": "https://picsum.photos/640/480/?image=330"
    },
    {
      "id": "bdc9c921-488d-44e6-9783-1d408062d8fd",
      "sku": "3852749467890",
      "name": "Fantastic Steel Ball",
      "description": "Rerum neque rerum sapiente repellendus magni impedit. Veniam soluta eum molestiae sit aut. Culpa voluptates repellat dolores eaque ut.",
      "price": 907.3491964528632,
      "quantity": 59,
      "tags": [
        "Garden",
        "Electronics",
        "Kids"
      ],
      "isDiscounted": true,
      "discountPercentage": 19.297008420852485,
      "imageUrl": "https://picsum.photos/640/480/?image=648"
    },
    {
      "id": "26494db0-dfaf-4d86-a8b2-ba07bdc53372",
      "sku": "0040352139317",
      "name": "Fantastic Concrete Cheese",
      "description": "At aliquam impedit adipisci consequatur perspiciatis voluptas et illum nam. Dolorem possimus vitae autem molestiae qui. Nam consequuntur consequuntur. Minima officia ut molestiae quos placeat saepe.",
      "price": 763.5979854117355,
      "quantity": 58,
      "tags": [
        "Health",
        "Games",
        "Tools"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=557"
    },
    {
      "id": "ef443163-273a-4624-ac7a-a3338e59fd1c",
      "sku": "9497144290885",
      "name": "Rustic Metal Soap",
      "description": "Ut nisi voluptate qui dicta. Neque expedita assumenda ipsam eos rerum a dolorum iste. Accusamus libero omnis commodi earum quam consequatur in. Qui dignissimos maiores in doloribus tempora sint.",
      "price": 282.40645515683485,
      "quantity": 0,
      "tags": [
        "Tools",
        "Games",
        "Industrial"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=850"
    },
    {
      "id": "f6c297b8-c29b-456e-919c-6ba509dc3997",
      "sku": "9844376501744",
      "name": "Intelligent Steel Salad",
      "description": "Numquam qui odio quo est. Sequi consequatur quisquam. Ut corporis dolore aperiam praesentium maxime autem optio eum.",
      "price": 601.808549567543,
      "quantity": 23,
      "tags": [
        "Industrial",
        "Baby",
        "Shoes"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=516"
    },
    {
      "id": "ac9fe289-4ca6-4878-a0c1-d6c3d5f92a49",
      "sku": "8827507773808",
      "name": "Intelligent Rubber Fish",
      "description": "Architecto accusamus veniam sunt exercitationem temporibus accusamus. Quos aut magnam harum consectetur repudiandae quasi. Ipsam ut adipisci id sit. Sit voluptatem error in nostrum consequatur. Non sit nostrum maiores vel placeat. Voluptas quia perferendis aut.",
      "price": 766.4561672605269,
      "quantity": 38,
      "tags": [
        "Automotive",
        "Toys",
        "Shoes"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=549"
    },
    {
      "id": "230de66b-17be-4077-a960-b5df248b95b1",
      "sku": "2228669691898",
      "name": "Gorgeous Plastic Gloves",
      "description": "Sed sit iste similique ad possimus modi eius rerum. Aut est sint quidem. Repellat quidem rerum vero. Quos necessitatibus eaque eum saepe.",
      "price": 6.520400964817012,
      "quantity": 0,
      "tags": [
        "Movies",
        "Beauty",
        "Outdoors"
      ],
      "isDiscounted": true,
      "discountPercentage": 21.267657722639065,
      "imageUrl": "https://picsum.photos/640/480/?image=1038"
    },
    {
      "id": "eea94d40-7f2e-488e-b75a-e921b68bd835",
      "sku": "8645734049813",
      "name": "Incredible Soft Chicken",
      "description": "Est soluta vero quis dolor qui autem laborum a delectus. Quisquam iste odit ipsum. Eum explicabo sequi sed doloremque. Facere quos eligendi tenetur. Enim natus ratione assumenda soluta aut tenetur.",
      "price": 233.4071780822858,
      "quantity": 91,
      "tags": [
        "Games",
        "Baby",
        "Health"
      ],
      "isDiscounted": true,
      "discountPercentage": 11.26982583283445,
      "imageUrl": "https://picsum.photos/640/480/?image=526"
    },
    {
      "id": "2c056269-3f0d-4875-bc30-08f48f43e215",
      "sku": "6558949735070",
      "name": "Practical Frozen Cheese",
      "description": "Aut ipsam aperiam eos. Enim temporibus libero perspiciatis. Quis sint libero. Unde ipsum dolorum animi atque veritatis quas.",
      "price": 477.1460526299038,
      "quantity": 74,
      "tags": [
        "Computers",
        "Garden",
        "Kids"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=10"
    },
    {
      "id": "5aa17792-d71d-4afe-8615-3d6fb2e72af0",
      "sku": "5140200150213",
      "name": "Generic Metal Chair",
      "description": "Tenetur maxime vero blanditiis quo amet. Labore doloremque eius non et. Dicta sit voluptatem delectus at et perspiciatis recusandae. Ducimus sint in iure sunt fugiat eius consequuntur veniam.",
      "price": 735.5610525384607,
      "quantity": 79,
      "tags": [
        "Home",
        "Electronics",
        "Sports"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=276"
    },
    {
      "id": "e8ccc169-f9b7-458d-a0e9-d873b57a6e1f",
      "sku": "0576735027792",
      "name": "Rustic Fresh Bike",
      "description": "Ab ut impedit autem optio nisi quo. Ullam vitae ab dolor. Qui atque quae officia ut architecto molestiae distinctio.",
      "price": 116.67858414246002,
      "quantity": 47,
      "tags": [
        "Electronics",
        "Shoes",
        "Grocery"
      ],
      "isDiscounted": true,
      "discountPercentage": 8.21074889871805,
      "imageUrl": "https://picsum.photos/640/480/?image=431"
    },
    {
      "id": "c91619ee-2d4a-480f-86cf-01ed2cb41a4a",
      "sku": "1361863242051",
      "name": "Practical Fresh Salad",
      "description": "Qui earum numquam totam deleniti eligendi aspernatur voluptatibus. Cum nihil ut sed numquam nesciunt quia vitae. Sed doloremque nulla repudiandae fuga et quae nihil.",
      "price": 687.190203552322,
      "quantity": 59,
      "tags": [
        "Industrial",
        "Automotive",
        "Grocery"
      ],
      "isDiscounted": true,
      "discountPercentage": 37.66901144337063,
      "imageUrl": "https://picsum.photos/640/480/?image=13"
    },
    {
      "id": "4c988d0c-0a9a-4a7a-8c13-964fe657a330",
      "sku": "8548673736427",
      "name": "Handmade Fresh Gloves",
      "description": "Id ut voluptas possimus qui vel aliquam dolores rerum mollitia. Maxime et accusamus alias id qui voluptas. Sapiente qui est eaque dolor aperiam unde rerum. Facilis exercitationem adipisci enim. Iure soluta quibusdam quasi a.",
      "price": 249.44493643012783,
      "quantity": 4,
      "tags": [
        "Health",
        "Tools",
        "Beauty"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=437"
    },
    {
      "id": "9dbafc28-e95d-4ac8-bce1-655e9f83a299",
      "sku": "3421889618709",
      "name": "Fantastic Concrete Chips",
      "description": "Molestiae officiis ipsum consequatur saepe error deleniti ipsam. Quibusdam at aliquid. Voluptatibus voluptas eaque quo eaque perspiciatis voluptatem. Aut nisi nulla quisquam.",
      "price": 898.8438186616631,
      "quantity": 45,
      "tags": [
        "Games",
        "Outdoors",
        "Kids"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=1014"
    },
    {
      "id": "9e64ad06-85b0-4afe-a902-ef86a85354ce",
      "sku": "7314080586006",
      "name": "Intelligent Wooden Mouse",
      "description": "Tempora consectetur deleniti non eligendi non. Aliquam exercitationem est sed. Fugiat autem corporis expedita quam voluptatum laudantium pariatur non ab. Doloribus autem consequatur et.",
      "price": 818.2516173405285,
      "quantity": 62,
      "tags": [
        "Games",
        "Sports",
        "Industrial"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=693"
    },
    {
      "id": "ddcba667-d0d4-41e0-98c9-404bc974aed4",
      "sku": "5352294535412",
      "name": "Intelligent Plastic Cheese",
      "description": "Minus eaque autem culpa reiciendis eius ipsum in omnis quam. Qui delectus cumque reprehenderit mollitia sit velit corporis. Odit molestiae quasi ipsum sed.",
      "price": 464.1908968248488,
      "quantity": 4,
      "tags": [
        "Home",
        "Computers",
        "Beauty"
      ],
      "isDiscounted": true,
      "discountPercentage": 43.44846151813948,
      "imageUrl": "https://picsum.photos/640/480/?image=344"
    },
    {
      "id": "6b5daf03-e934-4b41-87d3-ecc1115a6326",
      "sku": "7012095559915",
      "name": "Tasty Cotton Tuna",
      "description": "Nihil fugit excepturi non. Cumque quaerat ratione qui quod sequi. Ex rerum molestias et id.",
      "price": 241.47910466942608,
      "quantity": 84,
      "tags": [
        "Movies",
        "Music",
        "Music"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=860"
    },
    {
      "id": "629905d4-d556-44ee-a9e1-39292b347d6c",
      "sku": "6180243682691",
      "name": "Licensed Plastic Cheese",
      "description": "Earum quos facere omnis dolor repellendus. Corporis autem est voluptas. Ea omnis nulla velit tempore quidem autem. Blanditiis autem excepturi atque et autem consequatur enim laudantium iusto. Possimus maxime at ea reiciendis corrupti sunt aut repudiandae quaerat.",
      "price": 299.45790307480945,
      "quantity": 29,
      "tags": [
        "Clothing",
        "Outdoors",
        "Automotive"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=1031"
    },
    {
      "id": "f80c8466-fa97-41ce-851d-e86b0a0393b7",
      "sku": "3497626930489",
      "name": "Tasty Steel Mouse",
      "description": "Excepturi accusamus a et odio ipsam ipsam sunt cupiditate. Libero voluptates nostrum quisquam vel esse ipsum. Voluptatem tenetur corporis quia consequatur. Aperiam nesciunt rerum laudantium eveniet nemo. Tempore ratione cum nemo assumenda labore similique omnis.",
      "price": 825.8849504681897,
      "quantity": 16,
      "tags": [
        "Beauty",
        "Garden",
        "Garden"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=923"
    },
    {
      "id": "379a402f-e3e3-4a44-8ee1-49f8539b9798",
      "sku": "7948243010321",
      "name": "Incredible Granite Mouse",
      "description": "Culpa laudantium a veritatis ratione. Beatae voluptas dolorem ex nobis velit magni minus. Voluptate voluptatum dolorem.",
      "price": 838.6678936063406,
      "quantity": 85,
      "tags": [
        "Movies",
        "Industrial",
        "Garden"
      ],
      "isDiscounted": true,
      "discountPercentage": 11.571268864931666,
      "imageUrl": "https://picsum.photos/640/480/?image=384"
    },
    {
      "id": "f9d12c2c-0625-49e7-9599-bbe2e566f7b6",
      "sku": "6172438621073",
      "name": "Sleek Frozen Pizza",
      "description": "Magnam est sit. Placeat dignissimos hic quam vel. Est dignissimos optio commodi numquam. Pariatur reprehenderit saepe veritatis recusandae et tempora voluptas ducimus.",
      "price": 926.7184307410412,
      "quantity": 83,
      "tags": [
        "Beauty",
        "Kids",
        "Electronics"
      ],
      "isDiscounted": true,
      "discountPercentage": 34.54036017424715,
      "imageUrl": "https://picsum.photos/640/480/?image=709"
    },
    {
      "id": "f42d11e7-80e9-44f2-930b-7e88abb2e186",
      "sku": "4799838233112",
      "name": "Tasty Wooden Towels",
      "description": "Ex porro est perspiciatis. Culpa voluptatibus libero et debitis quam quasi quia nemo. Quia sint exercitationem sit velit fuga quo. Est non voluptas incidunt minus explicabo nihil.",
      "price": 151.61958668643152,
      "quantity": 94,
      "tags": [
        "Health",
        "Electronics",
        "Baby"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=1055"
    },
    {
      "id": "79096841-99ff-4f9b-bbc1-8d6618d3fdd0",
      "sku": "1622987860287",
      "name": "Incredible Cotton Computer",
      "description": "Atque eaque tempore possimus error dignissimos voluptatibus ut voluptatem quia. Quis tenetur qui dolor. Et quia occaecati. Eum quae aut omnis assumenda quaerat sapiente aut.",
      "price": 432.1324143503963,
      "quantity": 31,
      "tags": [
        "Automotive",
        "Baby",
        "Industrial"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=280"
    },
    {
      "id": "f1dab9b0-5301-4290-af26-186fb5aca922",
      "sku": "4792449308026",
      "name": "Tasty Wooden Chips",
      "description": "Maxime id dolorum labore consequuntur nisi officiis aut similique deleniti. Qui aut itaque nisi non dolor sed tempora quisquam nulla. Est nemo et sint ex dolorem. Neque commodi nemo voluptate in incidunt accusantium.",
      "price": 501.50324519290365,
      "quantity": 39,
      "tags": [
        "Baby",
        "Books",
        "Health"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=1007"
    },
    {
      "id": "628c4c23-3032-418b-861b-e3ec699bb4b9",
      "sku": "9696621942921",
      "name": "Sleek Metal Mouse",
      "description": "Sint ad rem. Iste ea est numquam. Alias sint repellendus quia. Et sit est sit assumenda. Exercitationem vel quae et rerum.",
      "price": 529.6793590671112,
      "quantity": 53,
      "tags": [
        "Outdoors",
        "Books",
        "Outdoors"
      ],
      "isDiscounted": true,
      "discountPercentage": 49.31487476987913,
      "imageUrl": "https://picsum.photos/640/480/?image=1032"
    },
    {
      "id": "57a38ae9-ea4c-4e66-abac-fdb694381c82",
      "sku": "5841574529253",
      "name": "Unbranded Wooden Ball",
      "description": "Ullam ea quia ea odit dolores molestias vitae dolore. Et autem aliquam rerum quam aliquid velit voluptatum laudantium. Incidunt delectus officia et est provident officiis accusamus. Nam voluptates voluptatem magni et quia omnis error provident accusamus. Earum iure qui vel et praesentium. Veniam nesciunt omnis.",
      "price": 979.2856384769636,
      "quantity": 50,
      "tags": [
        "Computers",
        "Kids",
        "Garden"
      ],
      "isDiscounted": true,
      "discountPercentage": 29.76320980786777,
      "imageUrl": "https://picsum.photos/640/480/?image=509"
    },
    {
      "id": "7a0246ed-5593-4e02-9c83-03edf6c9e8fe",
      "sku": "7846900601496",
      "name": "Licensed Rubber Pizza",
      "description": "Incidunt quibusdam voluptates. Fuga quasi deserunt eum est assumenda. Et optio doloremque. Aut dolor est explicabo placeat.",
      "price": 676.8490967441425,
      "quantity": 92,
      "tags": [
        "Automotive",
        "Industrial",
        "Home"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=534"
    },
    {
      "id": "0f1f51f7-ad9b-48bc-9ec5-ca84e5874378",
      "sku": "8494486878567",
      "name": "Rustic Soft Gloves",
      "description": "Ullam vel corrupti sit maxime odit iusto aliquid neque. Voluptatem aut exercitationem. Quo libero aliquam quod. Qui doloremque hic magni qui perferendis. Maiores non distinctio.",
      "price": 96.73209799565753,
      "quantity": 11,
      "tags": [
        "Books",
        "Industrial",
        "Tools"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=596"
    },
    {
      "id": "31e1d955-7d49-488c-b63d-3430d4b60f3d",
      "sku": "6500219792573",
      "name": "Generic Frozen Chicken",
      "description": "Ex nesciunt ea voluptatem. Ut libero et magni nisi. Officia hic distinctio error aperiam.",
      "price": 341.9985474699087,
      "quantity": 80,
      "tags": [
        "Grocery",
        "Home",
        "Computers"
      ],
      "isDiscounted": true,
      "discountPercentage": 49.62879572782826,
      "imageUrl": "https://picsum.photos/640/480/?image=455"
    },
    {
      "id": "0dc54e57-9db4-4434-97eb-3b3f93c4e4e1",
      "sku": "1611707097133",
      "name": "Handmade Frozen Pizza",
      "description": "Enim soluta est quia ad doloribus sapiente. Voluptates dolor a tenetur excepturi sunt. Est vero ad qui maiores pariatur excepturi eos. Molestiae quo consequatur qui. In sit et architecto delectus.",
      "price": 612.2622218548762,
      "quantity": 69,
      "tags": [
        "Books",
        "Jewelery",
        "Electronics"
      ],
      "isDiscounted": true,
      "discountPercentage": 25.24750300053281,
      "imageUrl": "https://picsum.photos/640/480/?image=473"
    },
    {
      "id": "6d006412-b031-44ca-86c0-f0e89403bc26",
      "sku": "2053793443675",
      "name": "Practical Wooden Soap",
      "description": "Architecto voluptatum aperiam vel voluptates quaerat a quia quo. Ex aut sit adipisci natus. Quis velit nemo ea harum. Labore nulla at nihil sed nulla. Impedit totam voluptatem praesentium enim temporibus officia.",
      "price": 731.9002473084823,
      "quantity": 96,
      "tags": [
        "Baby",
        "Toys",
        "Jewelery"
      ],
      "isDiscounted": true,
      "discountPercentage": 7.523508337031295,
      "imageUrl": "https://picsum.photos/640/480/?image=643"
    },
    {
      "id": "d04207cb-fe1a-46a4-95c4-80a8b1c78654",
      "sku": "2355293390795",
      "name": "Unbranded Frozen Salad",
      "description": "Officia nulla iusto aut nobis. Quos voluptatibus quis et labore ad est eos necessitatibus. Eos et nulla sunt eveniet ipsa impedit est non. Repellat ex voluptates sed vitae. Pariatur totam est alias.",
      "price": 541.0289088504612,
      "quantity": 60,
      "tags": [
        "Baby",
        "Games",
        "Baby"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=462"
    },
    {
      "id": "716d75ac-4606-4707-83f8-6ea760971f7c",
      "sku": "7178201831006",
      "name": "Ergonomic Rubber Gloves",
      "description": "Quas et et soluta rerum omnis perferendis. Possimus consectetur rerum reiciendis alias sapiente. Itaque nulla doloremque facere. Aut fugiat doloribus cupiditate aut voluptatem ut.",
      "price": 837.057758179096,
      "quantity": 81,
      "tags": [
        "Home",
        "Books",
        "Movies"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=735"
    },
    {
      "id": "6556a237-d12c-4db7-ac69-3d97079fc5d6",
      "sku": "9268391239626",
      "name": "Licensed Metal Ball",
      "description": "Impedit id illum pariatur voluptas. Doloremque suscipit enim doloremque voluptas non distinctio. Sunt perspiciatis possimus molestiae odio quia aut autem dolore et. In aut officiis et ipsam. Voluptas laboriosam quia repellat et. Quidem nisi aut aut et velit aliquid.",
      "price": 977.3328760370787,
      "quantity": 11,
      "tags": [
        "Music",
        "Automotive",
        "Books"
      ],
      "isDiscounted": true,
      "discountPercentage": 33.185121639489935,
      "imageUrl": "https://picsum.photos/640/480/?image=370"
    },
    {
      "id": "f4916661-b9b1-41ce-b79b-1ef9fa66a6b3",
      "sku": "6893145332211",
      "name": "Ergonomic Rubber Pants",
      "description": "Eum eum dolor sint quia blanditiis expedita incidunt aspernatur. Facere voluptatem et hic dolores ea vero. Laborum provident veniam. Libero ut quod expedita qui a et velit.",
      "price": 891.4871894107703,
      "quantity": 13,
      "tags": [
        "Games",
        "Movies",
        "Tools"
      ],
      "isDiscounted": true,
      "discountPercentage": 27.223556602914837,
      "imageUrl": "https://picsum.photos/640/480/?image=685"
    },
    {
      "id": "7ca4e1f0-0cea-4c32-b7dd-94297c009b17",
      "sku": "3410778743127",
      "name": "Awesome Soft Cheese",
      "description": "Quasi a est sed rerum et dicta provident. Error dicta error sit consequatur atque consequatur. Quaerat dolor est quia atque sint delectus voluptas.",
      "price": 577.598086153607,
      "quantity": 13,
      "tags": [
        "Games",
        "Movies",
        "Beauty"
      ],
      "isDiscounted": true,
      "discountPercentage": 36.94471426205901,
      "imageUrl": "https://picsum.photos/640/480/?image=262"
    },
    {
      "id": "f8b2c82e-0f13-42b9-9e30-4dee5c833b1a",
      "sku": "9077561348613",
      "name": "Unbranded Plastic Fish",
      "description": "Dolorum deleniti non quia dicta dolore. Debitis sit sed et voluptatem et dolores sapiente aut. Qui voluptas odit est. Rerum quia dignissimos saepe sed.",
      "price": 40.12057358410718,
      "quantity": 77,
      "tags": [
        "Games",
        "Grocery",
        "Toys"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=814"
    },
    {
      "id": "0db7d586-96d7-4424-84b1-70d074212d33",
      "sku": "5782500393251",
      "name": "Tasty Frozen Fish",
      "description": "Laudantium rerum est fuga sed debitis delectus fugit totam. Et mollitia necessitatibus iure vero at consequatur consequatur aut. Eos illum quasi aut aspernatur ad eum ipsum ut facilis. Accusantium pariatur quo quisquam debitis nihil voluptatibus veritatis quibusdam fugiat. A vero est et cupiditate odit quisquam incidunt.",
      "price": 726.6301733286315,
      "quantity": 74,
      "tags": [
        "Jewelery",
        "Grocery",
        "Home"
      ],
      "isDiscounted": true,
      "discountPercentage": 7.389350576081628,
      "imageUrl": "https://picsum.photos/640/480/?image=551"
    },
    {
      "id": "e06dc18c-c7ff-4a18-823e-f441f26e7429",
      "sku": "8931370790173",
      "name": "Fantastic Rubber Bike",
      "description": "Dolor distinctio dolores minus. Ipsa officia vel officiis architecto quia repellendus fugiat error et. Debitis aut quidem dolorum expedita atque distinctio rem.",
      "price": 910.5739263854448,
      "quantity": 94,
      "tags": [
        "Health",
        "Jewelery",
        "Tools"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=1005"
    },
    {
      "id": "31987a7d-75b7-4e71-9e8f-ab2c315de324",
      "sku": "1746238182611",
      "name": "Practical Steel Computer",
      "description": "Qui velit enim dolorem. Omnis aspernatur modi. Tempora non dolorem deserunt. Amet eos sit velit optio.",
      "price": 815.8508795645763,
      "quantity": 79,
      "tags": [
        "Jewelery",
        "Tools",
        "Electronics"
      ],
      "isDiscounted": true,
      "discountPercentage": 32.592977659115505,
      "imageUrl": "https://picsum.photos/640/480/?image=587"
    },
    {
      "id": "0d8cd30d-03dd-4870-9a09-6d8a0c99900a",
      "sku": "5037560914909",
      "name": "Rustic Plastic Chicken",
      "description": "Quas magni aut et nostrum debitis neque molestias. Et nulla sed. Voluptatem maxime odio sequi nam saepe illo aut pariatur voluptatem. Sit fuga aliquam magni. Fugit sint consequatur id. Quis tenetur voluptas laudantium non amet porro dolorem quo sint.",
      "price": 731.370979375509,
      "quantity": 17,
      "tags": [
        "Home",
        "Automotive",
        "Home"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=85"
    },
    {
      "id": "8125dfcd-31dc-4821-8553-41aa35e8a2a6",
      "sku": "8728806441798",
      "name": "Rustic Rubber Hat",
      "description": "Eos hic quia corporis quidem et sunt impedit quasi. Deleniti vero repellat earum. Et corrupti quidem a omnis minus. Saepe modi nihil assumenda rerum laudantium aut odio. Fuga temporibus sequi dignissimos aut magnam harum. Numquam quaerat ab eum ut omnis corrupti molestias.",
      "price": 85.3475859438462,
      "quantity": 54,
      "tags": [
        "Beauty",
        "Toys",
        "Industrial"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=736"
    },
    {
      "id": "3c3da901-11a8-4fbf-b574-90d39fe71787",
      "sku": "2577141412545",
      "name": "Handmade Soft Bacon",
      "description": "Eveniet voluptatem et repellat ipsum voluptates et maiores repudiandae blanditiis. Velit in dolor. Ipsam magnam modi itaque.",
      "price": 223.8736428984679,
      "quantity": 59,
      "tags": [
        "Books",
        "Kids",
        "Music"
      ],
      "isDiscounted": true,
      "discountPercentage": 5.48776678871587,
      "imageUrl": "https://picsum.photos/640/480/?image=958"
    },
    {
      "id": "9a3668ac-87d5-4bef-bc8d-72a089d5831f",
      "sku": "9207227475999",
      "name": "Tasty Frozen Pizza",
      "description": "Ducimus ut sunt. Aut voluptatibus id reiciendis mollitia qui placeat. Quam vel aspernatur natus dolore. Error cumque assumenda dolorem harum eveniet.",
      "price": 232.63139727054693,
      "quantity": 28,
      "tags": [
        "Kids",
        "Grocery",
        "Toys"
      ],
      "isDiscounted": true,
      "discountPercentage": 43.583257382770206,
      "imageUrl": "https://picsum.photos/640/480/?image=1073"
    },
    {
      "id": "4454db22-fdd2-4d55-8e62-fce504559185",
      "sku": "9161260813135",
      "name": "Ergonomic Granite Gloves",
      "description": "Vero omnis accusamus praesentium labore eum dolor placeat omnis dolorem. Facere fugit nostrum alias fugit eum quo. Mollitia laboriosam doloremque repellat ut similique. Labore voluptatem et porro error.",
      "price": 577.680914193627,
      "quantity": 63,
      "tags": [
        "Home",
        "Clothing",
        "Music"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=1001"
    },
    {
      "id": "9b7d3579-c5b6-4c3d-abfa-d68bdea6d848",
      "sku": "3624795756293",
      "name": "Practical Wooden Fish",
      "description": "Voluptatem id et rerum quia et velit sapiente eligendi dignissimos. Quia distinctio cum laudantium doloribus tempore veritatis iure eum. Exercitationem fuga laudantium cumque modi accusamus est dolore est. Quaerat minima sit inventore voluptas suscipit quod consequuntur.",
      "price": 79.31287514119789,
      "quantity": 38,
      "tags": [
        "Garden",
        "Industrial",
        "Automotive"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=994"
    },
    {
      "id": "d7a4a98c-ccc4-42ef-a51d-56e21996d4d8",
      "sku": "0221252438308",
      "name": "Handcrafted Fresh Tuna",
      "description": "Commodi et itaque atque qui quae atque repudiandae ut sit. Ut dolorem voluptatem voluptatem minus nobis illum. Eligendi sunt repellat voluptatum fuga. Non vel nobis. Et voluptatem corrupti neque et porro qui quae aperiam.",
      "price": 661.9953590113556,
      "quantity": 48,
      "tags": [
        "Shoes",
        "Garden",
        "Home"
      ],
      "isDiscounted": true,
      "discountPercentage": 14.486033160170424,
      "imageUrl": "https://picsum.photos/640/480/?image=77"
    },
    {
      "id": "a6282e13-f6b8-4f05-9c7e-242475a39754",
      "sku": "2472132745933",
      "name": "Awesome Steel Salad",
      "description": "Ut aliquid consequuntur rerum nihil asperiores fugit dolor eos. Voluptatem alias sapiente. Tenetur culpa ex omnis quia ut fugit. Aliquam corrupti voluptatibus maxime. Exercitationem cupiditate occaecati sunt fugiat reprehenderit.",
      "price": 209.97811663402902,
      "quantity": 80,
      "tags": [
        "Automotive",
        "Grocery",
        "Garden"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=348"
    },
    {
      "id": "95bbe503-698e-4b17-b7f7-24d711c24c9a",
      "sku": "9934783207275",
      "name": "Licensed Wooden Shirt",
      "description": "Commodi molestias magnam vel. Dolorum ea nostrum exercitationem pariatur eligendi sit in quidem ut. Est tempore adipisci quas. Quas error autem molestias quia et possimus accusantium assumenda aliquid.",
      "price": 413.3635443028765,
      "quantity": 47,
      "tags": [
        "Toys",
        "Games",
        "Electronics"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=796"
    },
    {
      "id": "682c7b7f-b94b-4d68-9dc4-203f7dc62664",
      "sku": "2266261048481",
      "name": "Handmade Plastic Salad",
      "description": "Et et hic quis consequuntur. Laudantium velit debitis asperiores. Et necessitatibus officiis est. Id inventore dolor numquam.",
      "price": 7.536336080934136,
      "quantity": 91,
      "tags": [
        "Automotive",
        "Tools",
        "Tools"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=652"
    },
    {
      "id": "70613e30-718d-4c95-8219-5228df34ab89",
      "sku": "5003586358263",
      "name": "Ergonomic Fresh Chips",
      "description": "Est quasi expedita architecto et qui. Architecto similique est et ratione. Voluptates non excepturi distinctio magnam id voluptatem et et nisi.",
      "price": 195.48022449910349,
      "quantity": 65,
      "tags": [
        "Music",
        "Garden",
        "Sports"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=244"
    },
    {
      "id": "0034ebaa-faaf-4468-82e8-30b9345f8f90",
      "sku": "5341293824719",
      "name": "Small Granite Soap",
      "description": "Ut saepe quas eum consequatur et aut unde consequatur. Ex qui suscipit cumque vel. Esse ex fugit aspernatur qui culpa eum nihil nesciunt.",
      "price": 407.25297877279877,
      "quantity": 94,
      "tags": [
        "Health",
        "Toys",
        "Automotive"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=208"
    },
    {
      "id": "7236a84b-0497-4b13-8435-8a3605679284",
      "sku": "5936175507141",
      "name": "Fantastic Rubber Pizza",
      "description": "Voluptas ullam vero omnis nesciunt quos quia ipsam non. Voluptate omnis ipsum molestias provident quis voluptas. Aspernatur illum dolorem facilis tempore et perspiciatis saepe. Ducimus eum modi omnis natus.",
      "price": 76.023397043258,
      "quantity": 71,
      "tags": [
        "Electronics",
        "Movies",
        "Jewelery"
      ],
      "isDiscounted": true,
      "discountPercentage": 37.133109966757445,
      "imageUrl": "https://picsum.photos/640/480/?image=60"
    },
    {
      "id": "9e72cf93-f9d5-434c-8b60-c845f63d665f",
      "sku": "2615004584753",
      "name": "Practical Rubber Fish",
      "description": "Accusamus veritatis iusto placeat adipisci nemo id. Officiis ab nam labore explicabo officia perferendis dolore enim. Quo sed aliquid quam minus alias. Corrupti magnam velit qui.",
      "price": 128.48021276055687,
      "quantity": 15,
      "tags": [
        "Books",
        "Toys",
        "Computers"
      ],
      "isDiscounted": true,
      "discountPercentage": 48.114468206886194,
      "imageUrl": "https://picsum.photos/640/480/?image=276"
    },
    {
      "id": "5c8d00f2-5e30-45a3-a317-205447eda7a0",
      "sku": "3928172704267",
      "name": "Practical Soft Ball",
      "description": "Sequi voluptates accusamus. Ut sit dolores. Enim vel quaerat iste qui velit nisi occaecati qui. Qui illum quia suscipit enim voluptas commodi ut.",
      "price": 869.7589973718522,
      "quantity": 83,
      "tags": [
        "Books",
        "Toys",
        "Automotive"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=7"
    },
    {
      "id": "e2cee0a9-0bdc-4265-82a2-6430abde3844",
      "sku": "2101145673345",
      "name": "Handmade Plastic Pants",
      "description": "Et illo natus culpa ex impedit. Qui eos id eum ut alias eligendi illum recusandae ut. Officia nihil officia. Nesciunt earum nisi unde.",
      "price": 141.82178205491428,
      "quantity": 46,
      "tags": [
        "Garden",
        "Electronics",
        "Shoes"
      ],
      "isDiscounted": true,
      "discountPercentage": 49.03596291220195,
      "imageUrl": "https://picsum.photos/640/480/?image=806"
    },
    {
      "id": "617ddc89-9309-43fe-aa06-8d9ff49b80e3",
      "sku": "3743057782099",
      "name": "Rustic Granite Salad",
      "description": "Quos est et beatae temporibus quis. Ipsum ut fuga sit alias id dolorum natus. Quam quibusdam laboriosam itaque illo laborum voluptas fugiat. Doloribus amet quisquam ut blanditiis ratione aspernatur.",
      "price": 753.9739599827766,
      "quantity": 65,
      "tags": [
        "Jewelery",
        "Kids",
        "Beauty"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=601"
    },
    {
      "id": "98a3a24d-b8c4-44f7-841b-686e35ba130b",
      "sku": "2991444611571",
      "name": "Handmade Frozen Sausages",
      "description": "Dolore repellendus et itaque dolorem modi. Consectetur et voluptatem molestiae ex hic officia fuga. Aliquid possimus quas ullam sint ut quia impedit. Qui at non ea et quam eum quas. Molestiae nihil consequatur rerum ut pariatur rerum harum. Aut cupiditate architecto et.",
      "price": 683.6975932933753,
      "quantity": 100,
      "tags": [
        "Baby",
        "Computers",
        "Toys"
      ],
      "isDiscounted": true,
      "discountPercentage": 45.77499779511188,
      "imageUrl": "https://picsum.photos/640/480/?image=726"
    },
    {
      "id": "babc5a0b-494c-4411-9a55-44eba7d09b78",
      "sku": "1665662033477",
      "name": "Awesome Steel Shirt",
      "description": "Accusantium dolorem deserunt labore. Sit aperiam sed qui consequatur. Voluptatem mollitia aut sed at consequatur atque cumque. Voluptatem veniam iste. Nulla cumque facere rerum. Consequatur eaque esse sit neque sed maiores dolores quibusdam consequatur.",
      "price": 433.2976651365839,
      "quantity": 23,
      "tags": [
        "Industrial",
        "Industrial",
        "Toys"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=350"
    },
    {
      "id": "a52b74af-9a8a-4384-86ca-1b564f8894d4",
      "sku": "0381845944255",
      "name": "Rustic Steel Tuna",
      "description": "Provident ad odio sapiente. Quasi et in molestias. Et cupiditate dolores eligendi. Nostrum veritatis qui nostrum. Cumque laborum eius modi consequatur. Tempora sed repudiandae sunt distinctio sed.",
      "price": 529.0450229838295,
      "quantity": 83,
      "tags": [
        "Games",
        "Electronics",
        "Garden"
      ],
      "isDiscounted": true,
      "discountPercentage": 28.709732433350286,
      "imageUrl": "https://picsum.photos/640/480/?image=796"
    },
    {
      "id": "c0ad370c-c005-4504-a4ac-4eeb355e817f",
      "sku": "4146688239946",
      "name": "Handcrafted Frozen Fish",
      "description": "Velit consequatur sint consequatur. Aliquid omnis quibusdam iure sit voluptas soluta occaecati. Iusto impedit culpa nihil incidunt est ratione quibusdam nulla. Ab dolor sed placeat totam ut facilis delectus fugit dolorem.",
      "price": 304.34729603047055,
      "quantity": 2,
      "tags": [
        "Garden",
        "Garden",
        "Shoes"
      ],
      "isDiscounted": true,
      "discountPercentage": 14.242798951464462,
      "imageUrl": "https://picsum.photos/640/480/?image=320"
    },
    {
      "id": "ae06e385-adf6-4fe1-bb6d-a31e2143ee9e",
      "sku": "8859479566096",
      "name": "Ergonomic Fresh Bike",
      "description": "In consectetur aspernatur est nobis expedita officia et suscipit quasi. Omnis omnis cum voluptatem minus vel eos voluptatem. Perferendis consequuntur asperiores quia possimus dolore inventore. Sit modi dolorum quis explicabo.",
      "price": 429.5788043762296,
      "quantity": 26,
      "tags": [
        "Jewelery",
        "Music",
        "Outdoors"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=430"
    },
    {
      "id": "579cd45a-b2fb-47d7-a7c2-bef2802b7426",
      "sku": "1531253761121",
      "name": "Incredible Metal Hat",
      "description": "Sit illum non. Qui ut numquam rerum provident nobis. Quasi libero quia repudiandae ut est quam omnis. Beatae dignissimos ab. Et quis rerum hic similique ex. Quae itaque iste inventore excepturi quod veniam saepe.",
      "price": 555.1737079419856,
      "quantity": 14,
      "tags": [
        "Shoes",
        "Beauty",
        "Toys"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=878"
    },
    {
      "id": "d59bc66d-864d-4829-b50a-7f0ec641b545",
      "sku": "0437366652701",
      "name": "Sleek Rubber Bacon",
      "description": "Rerum consequatur quas fugiat quisquam vel magni quidem qui doloribus. Impedit debitis sit officiis consectetur. Nulla hic libero corrupti iure. Earum qui in sunt sunt voluptatem et veniam eaque.",
      "price": 735.1072755332082,
      "quantity": 67,
      "tags": [
        "Garden",
        "Books",
        "Baby"
      ],
      "isDiscounted": true,
      "discountPercentage": 41.17039558456674,
      "imageUrl": "https://picsum.photos/640/480/?image=803"
    },
    {
      "id": "a73bcb28-3790-4651-ac2f-3122fa54463f",
      "sku": "9591087442674",
      "name": "Handmade Granite Bike",
      "description": "Assumenda et iste ipsam velit. Maxime soluta qui. Non quisquam aliquid neque dolore fugiat velit odit. Ducimus exercitationem optio. Et deleniti odit nobis. Autem corrupti dolores totam pariatur necessitatibus expedita veniam id.",
      "price": 463.7746839584946,
      "quantity": 37,
      "tags": [
        "Tools",
        "Industrial",
        "Grocery"
      ],
      "isDiscounted": true,
      "discountPercentage": 31.912517251152266,
      "imageUrl": "https://picsum.photos/640/480/?image=766"
    },
    {
      "id": "f26f2335-4d8c-495b-9d46-61d5843c2770",
      "sku": "5955767248194",
      "name": "Handmade Granite Gloves",
      "description": "Consectetur quod atque repellendus pariatur ut aliquid aspernatur voluptatem. Delectus fuga nemo quisquam eos dicta suscipit fugiat aut ratione. Sunt distinctio ipsa modi.",
      "price": 11.096056849109178,
      "quantity": 57,
      "tags": [
        "Industrial",
        "Shoes",
        "Garden"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=613"
    },
    {
      "id": "c82ecc1b-a9bf-43db-816a-744903bb7b10",
      "sku": "2342681109718",
      "name": "Small Wooden Hat",
      "description": "Qui non est amet sit amet. Aut corrupti doloribus molestias et ullam dolorem voluptatum accusantium. Est repudiandae explicabo tempora. Ducimus repellat tempore distinctio non dolor deserunt suscipit. Et amet sapiente voluptatibus voluptas in.",
      "price": 239.97938849645143,
      "quantity": 37,
      "tags": [
        "Baby",
        "Games",
        "Tools"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=899"
    },
    {
      "id": "4cb3a192-fd99-43f8-8747-f03ab2ebc8ea",
      "sku": "8660671908603",
      "name": "Sleek Rubber Shirt",
      "description": "Explicabo quibusdam voluptas laboriosam ex. Aut vero omnis adipisci ipsa. Iure sit quae alias. Totam alias saepe architecto excepturi.",
      "price": 456.886347650669,
      "quantity": 46,
      "tags": [
        "Baby",
        "Garden",
        "Grocery"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=151"
    },
    {
      "id": "cde0f57a-f3f6-4723-ac35-a8edd4d4a1eb",
      "sku": "5413882115833",
      "name": "Refined Fresh Salad",
      "description": "Reprehenderit sit alias distinctio laboriosam aut animi. Ipsum voluptas mollitia quisquam nihil qui laborum eum. Est voluptatem autem odio sit quam assumenda adipisci hic. Sunt nobis magnam magnam enim sit distinctio eaque veritatis dolore. Quisquam temporibus amet sit cupiditate nulla.",
      "price": 380.63385805304205,
      "quantity": 13,
      "tags": [
        "Shoes",
        "Sports",
        "Home"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=622"
    },
    {
      "id": "e29947af-ec98-4c79-a534-b6249f24a528",
      "sku": "6561417745921",
      "name": "Handmade Granite Cheese",
      "description": "Voluptatem amet rerum ut facilis labore ullam dolore doloremque. Quo quod et voluptas ut quia quo sequi qui. Vel blanditiis aperiam itaque tempore quidem adipisci eaque vel consequatur.",
      "price": 609.9457234265448,
      "quantity": 20,
      "tags": [
        "Grocery",
        "Beauty",
        "Music"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=559"
    },
    {
      "id": "d62eb3b8-a138-4bf8-b671-7005822b7c67",
      "sku": "2366051711776",
      "name": "Fantastic Granite Bacon",
      "description": "Animi necessitatibus quibusdam necessitatibus. A non qui eligendi cupiditate. Quis consectetur dolorem aperiam ea nulla facilis necessitatibus non.",
      "price": 868.7535395697365,
      "quantity": 69,
      "tags": [
        "Jewelery",
        "Jewelery",
        "Movies"
      ],
      "isDiscounted": true,
      "discountPercentage": 18.26987349737385,
      "imageUrl": "https://picsum.photos/640/480/?image=630"
    },
    {
      "id": "04350d35-1c39-4fb8-9ce7-c74be2ab3b71",
      "sku": "2146350528134",
      "name": "Rustic Soft Gloves",
      "description": "Repellendus ea cupiditate ut quod tempora qui veritatis sit fugiat. Omnis sit in voluptatem. Voluptas aut dolores ut officiis est dolorem nemo quo. Qui blanditiis numquam fugiat sit et ex aperiam cumque. Ipsam molestiae quis quidem dicta. Tempora officia vel quia.",
      "price": 711.1247674941698,
      "quantity": 12,
      "tags": [
        "Jewelery",
        "Grocery",
        "Movies"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=346"
    },
    {
      "id": "cce6f5f9-9dc7-487a-a308-ca942b1531c7",
      "sku": "3991382771503",
      "name": "Small Soft Ball",
      "description": "Fuga porro excepturi cum. Quis harum id enim. Delectus totam mollitia libero ratione aliquid. Expedita tempore sint et illo. Ad sint non adipisci consequuntur enim asperiores. Quia qui autem aut alias ad doloribus.",
      "price": 985.2604244856002,
      "quantity": 83,
      "tags": [
        "Sports",
        "Garden",
        "Toys"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=390"
    },
    {
      "id": "2cfee7b9-e2ff-4dcc-be01-a965ddfcdddf",
      "sku": "1953532518897",
      "name": "Gorgeous Steel Cheese",
      "description": "Quia laboriosam labore recusandae tenetur hic. Non suscipit porro sed et amet qui asperiores quibusdam dolor. Autem veniam vero aperiam dolorum ut eum dolor perferendis. Enim magnam consequatur perspiciatis voluptatem sed nemo sit qui velit.",
      "price": 50.129123409414476,
      "quantity": 30,
      "tags": [
        "Tools",
        "Outdoors",
        "Health"
      ],
      "isDiscounted": true,
      "discountPercentage": 25.30165534435904,
      "imageUrl": "https://picsum.photos/640/480/?image=293"
    },
    {
      "id": "809e6e9b-afdf-4054-bdf8-1a493cd9ae2b",
      "sku": "6518013665160",
      "name": "Rustic Frozen Tuna",
      "description": "Nam aliquid ut qui libero est. Optio nobis facilis. Eum aut accusamus quo quos.",
      "price": 990.2658453170885,
      "quantity": 93,
      "tags": [
        "Home",
        "Outdoors",
        "Toys"
      ],
      "isDiscounted": true,
      "discountPercentage": 26.38484681484931,
      "imageUrl": "https://picsum.photos/640/480/?image=393"
    },
    {
      "id": "e950f55d-e2b4-407e-86eb-4dca076fad2c",
      "sku": "1096715014059",
      "name": "Incredible Plastic Hat",
      "description": "Dolores asperiores officia magni dolorem ipsa quis sunt animi. Ut debitis dolor nihil. In sed et quam ea et inventore. Porro totam aut.",
      "price": 828.919125386731,
      "quantity": 72,
      "tags": [
        "Garden",
        "Jewelery",
        "Sports"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=597"
    },
    {
      "id": "c6570823-ff05-43a6-8d3d-cf58200133c0",
      "sku": "9539112315814",
      "name": "Unbranded Steel Chips",
      "description": "Molestiae exercitationem consequatur sunt quod. Consequatur voluptates nisi. Sed ex mollitia aut.",
      "price": 683.747643168665,
      "quantity": 9,
      "tags": [
        "Music",
        "Beauty",
        "Toys"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=166"
    },
    {
      "id": "c866a50e-6198-4e88-b8f6-e51df9a8e1db",
      "sku": "7302044739933",
      "name": "Fantastic Frozen Mouse",
      "description": "Consequatur et laborum voluptatem asperiores exercitationem quasi blanditiis pariatur debitis. Non velit accusantium dolores perspiciatis exercitationem temporibus molestiae consequatur pariatur. Dignissimos consectetur non quisquam ut. Odio distinctio minus nam dignissimos corrupti saepe.",
      "price": 819.2235774557961,
      "quantity": 97,
      "tags": [
        "Electronics",
        "Outdoors",
        "Movies"
      ],
      "isDiscounted": true,
      "discountPercentage": 14.754027977137104,
      "imageUrl": "https://picsum.photos/640/480/?image=204"
    },
    {
      "id": "a0df3872-0c9a-4973-acca-fd6d6fe3d597",
      "sku": "9195419404493",
      "name": "Licensed Wooden Fish",
      "description": "Voluptates est iure dolorum vitae veniam odio id. Libero minus accusantium aliquam illum voluptatem et sapiente ea illum. Enim sit et et et veniam quia. Error iste quos. Voluptatem nostrum nisi quo quo. Ut unde voluptatibus vitae et voluptas dolorum officia.",
      "price": 240.071052023606,
      "quantity": 11,
      "tags": [
        "Automotive",
        "Automotive",
        "Beauty"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=164"
    },
    {
      "id": "41d59f3c-a697-42c5-b740-01c2b5ae9cd2",
      "sku": "5202543971532",
      "name": "Intelligent Concrete Salad",
      "description": "Aut nihil et aspernatur. Soluta exercitationem dolores. Qui error repudiandae vitae est officiis explicabo. Eius ipsa sed quas. Earum accusamus est cumque enim distinctio dicta.",
      "price": 422.54110063523905,
      "quantity": 73,
      "tags": [
        "Automotive",
        "Music",
        "Computers"
      ],
      "isDiscounted": true,
      "discountPercentage": 34.84722999093917,
      "imageUrl": "https://picsum.photos/640/480/?image=939"
    },
    {
      "id": "c000566a-5b32-4b8a-8626-db5ad4df98a4",
      "sku": "7542593720659",
      "name": "Ergonomic Fresh Hat",
      "description": "Sint ut earum et consequuntur expedita sunt quis beatae. Fuga voluptates officiis aut illum. Id dolor similique est et expedita.",
      "price": 758.1724859446709,
      "quantity": 1,
      "tags": [
        "Shoes",
        "Electronics",
        "Games"
      ],
      "isDiscounted": true,
      "discountPercentage": 28.727946782314703,
      "imageUrl": "https://picsum.photos/640/480/?image=113"
    },
    {
      "id": "e4f37445-7b48-4713-8292-f22208edabbf",
      "sku": "8897280875501",
      "name": "Ergonomic Soft Sausages",
      "description": "Ipsam sit aliquam rerum veritatis nobis nulla repudiandae. Id voluptates repellendus. Distinctio veritatis temporibus voluptates ratione eum ullam nobis animi.",
      "price": 990.8626470815578,
      "quantity": 20,
      "tags": [
        "Health",
        "Tools",
        "Home"
      ],
      "isDiscounted": true,
      "discountPercentage": 17.022107092589337,
      "imageUrl": "https://picsum.photos/640/480/?image=1040"
    },
    {
      "id": "eb16590e-cc0b-43df-a007-1b08ee6589cc",
      "sku": "8959842092761",
      "name": "Unbranded Plastic Shirt",
      "description": "Autem ea dolores ab sit. Quia natus sint quos fuga tempore. Fugiat quis aliquam. Aperiam est natus fugiat optio laboriosam sapiente ad.",
      "price": 401.70188612114976,
      "quantity": 85,
      "tags": [
        "Toys",
        "Music",
        "Home"
      ],
      "isDiscounted": true,
      "discountPercentage": 25.37087043742585,
      "imageUrl": "https://picsum.photos/640/480/?image=934"
    },
    {
      "id": "e34a8c35-2a38-4c9e-86b9-0809f62b487d",
      "sku": "7961272731388",
      "name": "Handmade Soft Salad",
      "description": "Eligendi porro qui qui et quisquam aliquam id voluptatum at. Asperiores et ut. Vel autem facere nam vel dolore aliquid sunt nisi suscipit.",
      "price": 403.3687867841715,
      "quantity": 17,
      "tags": [
        "Automotive",
        "Sports",
        "Games"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=795"
    },
    {
      "id": "b29a1341-84ba-461a-bab1-d929618f6576",
      "sku": "6300653376649",
      "name": "Sleek Plastic Mouse",
      "description": "Hic laboriosam doloribus sit atque illum dolorem enim vel assumenda. Quo illo numquam fugit ut et fuga deserunt. Iusto ad ab sint autem.",
      "price": 205.46706606232686,
      "quantity": 83,
      "tags": [
        "Tools",
        "Jewelery",
        "Sports"
      ],
      "isDiscounted": true,
      "discountPercentage": 45.391412113006865,
      "imageUrl": "https://picsum.photos/640/480/?image=539"
    },
    {
      "id": "d6f6cdb5-ad8c-4873-8e25-0f2d2cdcde98",
      "sku": "8351604538874",
      "name": "Ergonomic Rubber Towels",
      "description": "Omnis esse quas numquam. Voluptatibus voluptas soluta modi officiis. Et veritatis dolorem perspiciatis illo voluptatibus ex possimus doloremque ex. Quos error dicta qui animi iste mollitia voluptas illo illum. Vel dolore illum expedita quas voluptas. Rerum ab consequatur voluptates.",
      "price": 686.0815046702468,
      "quantity": 34,
      "tags": [
        "Garden",
        "Sports",
        "Baby"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=584"
    },
    {
      "id": "3afb305a-82e5-435a-b7d6-81ce68e7b387",
      "sku": "2863145366978",
      "name": "Awesome Frozen Gloves",
      "description": "Hic accusantium voluptatem dolores eaque aut fuga et ratione. Neque et ut beatae. Corrupti voluptatem ut aliquid qui.",
      "price": 682.7026524923601,
      "quantity": 40,
      "tags": [
        "Jewelery",
        "Books",
        "Music"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=399"
    },
    {
      "id": "7e518d8a-9620-4bc0-929a-f522f9801647",
      "sku": "6360014053824",
      "name": "Handcrafted Plastic Sausages",
      "description": "Quos et et. Eos at in doloremque. Mollitia assumenda voluptatem labore.",
      "price": 680.1032806547955,
      "quantity": 7,
      "tags": [
        "Tools",
        "Electronics",
        "Beauty"
      ],
      "isDiscounted": true,
      "discountPercentage": 30.92148338860577,
      "imageUrl": "https://picsum.photos/640/480/?image=121"
    },
    {
      "id": "14528399-34ac-44d2-b876-2c1c24e89976",
      "sku": "7882972259111",
      "name": "Incredible Soft Keyboard",
      "description": "Adipisci dolorum quidem iusto alias voluptatem eius quaerat quia quam. Sed iusto facilis excepturi deserunt amet sint officiis. Velit natus sit molestiae repellendus dolores aut nihil sed quisquam. Nihil veritatis aut consequuntur at consequuntur dolor eos amet quos.",
      "price": 108.29455627650526,
      "quantity": 50,
      "tags": [
        "Industrial",
        "Outdoors",
        "Jewelery"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=809"
    },
    {
      "id": "42a854c0-3c2e-4b45-89a7-9b1b05958cdb",
      "sku": "0445438747132",
      "name": "Handmade Soft Shirt",
      "description": "Minima eum quibusdam quisquam aut corrupti voluptas. Distinctio nobis tempora illo est necessitatibus perferendis tempore. Ratione corrupti molestiae iure porro repellendus sint repellendus delectus. Et nesciunt aut et. Omnis error dolores inventore.",
      "price": 971.8784657148491,
      "quantity": 22,
      "tags": [
        "Clothing",
        "Garden",
        "Kids"
      ],
      "isDiscounted": false,
      "discountPercentage": 0,
      "imageUrl": "https://picsum.photos/640/480/?image=210"
    },
    {
      "id": "d1eb6222-6146-4430-b9f8-c10d6552a6b0",
      "sku": "8536543160823",
      "name": "Handmade Granite Pizza",
      "description": "Sint labore nemo cum rem. Voluptatem ab iste. Facere autem explicabo itaque ad et fugit.",
      "price": 805.7302531534666,
      "quantity": 1,
      "tags": [
        "Grocery",
        "Industrial",
        "Shoes"
      ],
      "isDiscounted": true,
      "discountPercentage": 17.619521781321055,
      "imageUrl": "https://picsum.photos/640/480/?image=963"
    }
  ];
  public isLoading: boolean = false;

  constructor(private _catalogItemService: CatelogItemRepository) {}

  public ngOnInit(): void {
    // this.isLoading = true;

    // // Automatically fetch token and load items
    // this._catalogItemService.fetchAccessToken().subscribe({
    //   next: () => {
    //     this.getItems();
    //   },
    //   error: (error) => {
    //     console.error('Error fetching access token:', error);
    //     this.isLoading = false;
    //   }
    // });
  }

  public getItems(): void {
    this._catalogItemService.getAllItems().subscribe({
      next: (result: Stock) => {
        this.items = result.items;
        // console.log(this.items);
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error fetching items:', error);
        this.isLoading = false;
      }
    });
  }
}
