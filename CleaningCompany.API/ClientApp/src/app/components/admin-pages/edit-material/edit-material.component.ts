import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Material } from '../../../models/interfaces/Materials/Material';
import { HelperService } from '../../../services/helper.service';
import { MaterialsService } from '../../../services/materials.service';

@Component({
  selector: 'app-edit-material',
  templateUrl: './edit-material.component.html',
  styleUrls: ['./edit-material.component.scss']
})
export class EditMaterialComponent implements OnInit {

  id: number;
  material: Material = {
    Id: 0,
    Name: null,
    Price: null
  }

  isCreate: boolean = false;

  materialForm: FormGroup;

  constructor(private helperService: HelperService,
    private materialService: MaterialsService,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = Number(params.id);
      if (isNaN(this.id)) {
        this.isCreate = true;
        this.fillForm();
      }
      else {
        this.getMaterial();
      }
    })
  }

  getMaterial() {
    this.materialService.getMaterialById(this.id).then(material => {
      this.material = material;

      this.fillForm();
    })
  }

  fillForm() {
    this.materialForm = this.fb.group({
      Name: this.fb.control(this.material.Name, Validators.required),
      Price: this.fb.control(this.material.Price, [Validators.required])
    });
  }

  goToMaterials() {
    this.router.navigate(['admin/materials']);
  }

  cancel() {
    this.goToMaterials();
  }

  save() {
    this.material.Name = this.materialForm.get('Name').value;
    this.material.Price = Number(this.materialForm.get('Price').value);

    if (this.isCreate) {
      this.createMaterial();
    }
    else {
      this.updateMaterial();
    }
  }

  createMaterial() {
    this.materialService.createMaterial(this.material).then(id => {
      this.material.Id = id;
      this.helperService.alert("Material was successfully created!");
      this.goToMaterials();
    })
  }

  updateMaterial() {
    this.materialService.updateMaterial(this.material).then(_ => {
      this.helperService.alert("Material was successfully updated!")
    })
  }

  deleteMaterial() {
    this.helperService.confirmPopup("", "Are you sure you want to delete this material?",).then(result => {
      if (result) {
        this.materialService.deleteMaterial(this.id).then(result => {
          this.helperService.alert("Material was successfully deleted!");
          this.goToMaterials();
        })
      }
    })
  }
}
