import { Injectable, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Model } from "../models/model.model";

@Injectable()
export class ModelService {
  constructor(
    private _client: HttpClient
  ) { }

  public get(): Observable<Array<Model>> {
    return this._client.get<{ models: Array<Model> }>(`/api/models`)
      .pipe(
        map(x => x.models)
      );
  }

  public getById(options: { modelId: number }): Observable<Model> {
    return this._client.get<{ model: Model }>(`/api/models/${options.modelId}`)
      .pipe(
        map(x => x.model)
      );
  }

  public remove(options: { model: Model }): Observable<void> {
    return this._client.delete<void>(`/api/models/${options.model.modelId}`);
  }

  public save(options: { model: Model }): Observable<{ modelId: number }> {
    return this._client.post<{ modelId: number }>(`/api/models`, { model: options.model });
  }
}
