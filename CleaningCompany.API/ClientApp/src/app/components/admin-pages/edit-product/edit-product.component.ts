import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductWithMaterials } from '../../../models/interfaces/Products/ProductWithMaterials';
import { HelperService } from '../../../services/helper.service';
import { ProductsService } from '../../../services/products.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {

  id: number;
  product: ProductWithMaterials = {
    Id: 0,
    BasePrice: null,
    Description: null,
    Name: null,
    Materials: [],
    Difficulty: null
  }

  productForm: FormGroup;

  isCreate: boolean = false;

  difficulties: SelectList[] = [
    {
      Id: 1,
      Name: "Low"
    },
    {
      Id: 2,
      Name: "Medium"
    },
    {
      Id: 3,
      Name: "Hard"
    }
  ];

  constructor(private router: Router,
    private route: ActivatedRoute,
    private productService: ProductsService,
    private fb: FormBuilder,
    private helperService: HelperService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params.id;

      if (isNaN(this.id)) {
        this.isCreate = true;
        this.fillForm();
      }
      else {
        this.getProduct();
      }
    })

    this.getMaterials();
  }

  getProduct() {
    this.productService.getProductById(this.id).then(product => {
      this.product = product;
      this.fillForm();
    })
  }

  getMaterials() {

  }

  fillForm() {
    this.productForm = this.fb.group({
      Name: this.fb.control(this.product.Name, Validators.required),
      Description: this.fb.control(this.product.Description),
      BasePrice: this.fb.control(this.product.BasePrice, Validators.required),
      Difficulty: this.fb.control(this.product.Difficulty, Validators.required)
    });
  }

  goToProducts() {
    this.router.navigate(['admin/products']);
  }

  cancel() {
    this.goToProducts();
  }

  save() {
    this.product = {
      ... this.productForm.value as ProductWithMaterials,
      Id: this.isCreate ? 0 : this.product.Id,
      BasePrice: Number(this.productForm.get('BasePrice').value),
      Materials: []
    }

    if (this.isCreate) {
      this.createProduct();
    }
    else {
      this.updateProduct();
    }
  }

  createProduct() {
    this.productService.createProduct(this.product).then(_ => {
      this.helperService.alert("Product was successfully created!");
      this.goToProducts();
    })
  }

  updateProduct() {
    this.productService.updateProduct(this.product).then(_ => {
      this.helperService.alert("Product was successfully updated!");
    })
  }

  deleteProduct() {
    this.helperService.confirmPopup("", "Are you sure you want to delete this product?").then(result => {
      if (result) {
        this.productService.deleteProduct(this.id).then(_ => {
          this.helperService.alert("Product was successfully deleted!");
          this.goToProducts();
        })
      }
    })
  }

}

export interface SelectList {
  Name: string;
  Id: number;
}
