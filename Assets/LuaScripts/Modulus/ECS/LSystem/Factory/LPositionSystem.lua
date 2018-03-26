LPositionSystem = SimpleClass(LSystem)

function LPositionSystem:__init_self()
    self.subscibe = LCompType.Position
end 

function LPositionSystem:__init()

end 

function LPositionSystem:onUpdate(lst)    
    for k,v in pairs(lst) do 
        local isNeed = v:isNeedUpdate()
        if isNeed then 
           local entity = EntityMgr:getEntity(v:getUid())
           if entity then 
              local dir = v.pos - entity.root.transform.position
              local isArrive = dir.magnitude <= 1
              local animComp = entity:getComp(LCompType.Animator)
              if animComp and animComp.anim then 
                 animComp.anim:SetFloat('tree',isArrive and 0 or 0.5)
              end
              if not isArrive then
                local angle = LuaExtend:getAngle(dir)
                entity:updateComp(LCompType.Rotation,angle)--面向
                local comp = entity:getComp(LCompType.CharacterController)
                if comp and comp.cc then 
                   comp.cc:SimpleMove(dir.normalized)--cc
                end 
              else
                entity:updateComp(LCompType.Animator,'idle')
                local comp = entity:getComp(LCompType.CharacterController)
                if comp and comp.cc then 
                   comp.cc:SimpleMove(Vector3(0,0,0))
                end 
                v.isArrive = true 
              end
           end
        end 
    end 
end 

function LPositionSystem:disposeComp(comp)

end